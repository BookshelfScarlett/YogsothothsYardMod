using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.Huds;
using YogsothothsYardMod.Menus.Managers;

namespace YogsothothsYardMod.Menus.MainMenu.Buttons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Terraria.ModLoader;

    /// <summary>
    /// 对tmod内部主菜单相关私有成员的反射访问封装
    /// <br>随着tmod版本更新可能会发生变动，这里一定得时刻查看情况</br>
    /// <br>记得编译的时候缓存为委托避免大量开销</br>
    /// </summary>
    public static class MenuReflect
    {
        #region 一堆构造基础
        public static readonly Type MenuLoaderType;
        public static readonly Type ModMenuType;

        public static readonly Func<object> CurrentMenu;
        public static readonly Func<IList> Menus;
        public static readonly Func<string[]> KnownMenus;

        public static readonly Func<object, bool> GetIsNew;
        public static readonly Action<object, bool> SetIsNew;
        public static readonly Func<object, bool> GetIsAvailable;
        public static readonly Func<object, string> GetFullName;
        public static readonly Func<object, string> GetDisplayName;

        public static readonly Func<bool> NotifyNewThemes;
        public static readonly Action<int> OffsetModMenu;
        #endregion

        //准备一个反射封装器
        static MenuReflect()
        {
            Assembly asm = typeof(ModLoader).Assembly;

            MenuLoaderType = asm.GetType("Terraria.ModLoader.MenuLoader");
            ModMenuType = asm.GetType("Terraria.ModLoader.ModMenu");

            if (MenuLoaderType == null || ModMenuType == null)
                throw new TypeLoadException("找不到 MenuLoader / ModMenu 类型，请检查 tModLoader 版本。");

            CurrentMenu = MakeStaticGetter<object>(MenuLoaderType, "CurrentMenu");
            Menus = MakeStaticGetter<IList>(MenuLoaderType, "menus");
            KnownMenus = MakeStaticGetter<string[]>(MenuLoaderType, "KnownMenus");

            GetIsNew = MakeInstanceGetter<bool>(ModMenuType, "IsNew");
            SetIsNew = MakeInstanceSetter<bool>(ModMenuType, "IsNew");
            GetIsAvailable = MakeInstanceGetter<bool>(ModMenuType, "IsAvailable");
            GetFullName = MakeInstanceGetter<string>(ModMenuType, "FullName");
            GetDisplayName = MakeInstanceGetter<string>(ModMenuType, "DisplayName");

            NotifyNewThemes = MakeStaticGetter<bool>(typeof(ModLoader), "notifyNewMainMenuThemes");
            OffsetModMenu = MakeStaticAction<int>(MenuLoaderType, "OffsetModMenu");
        }

        /// <summary>
        /// 通用访问器生成
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static MemberInfo FindMember(Type type, string name)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic
                                     | BindingFlags.Static | BindingFlags.Instance
                                     | BindingFlags.FlattenHierarchy;
            return (MemberInfo)type.GetProperty(name, flags) ?? type.GetField(name, flags);
        }

        private static Func<T> MakeStaticGetter<T>(Type type, string name)
        {
            MemberInfo member = FindMember(type, name)
                ?? throw new MissingMemberException(type.FullName, name);

            Expression access = member is PropertyInfo p
                ? Expression.Property(null, p)
                : Expression.Field(null, (FieldInfo)member);

            return Expression.Lambda<Func<T>>(Expression.Convert(access, typeof(T))).Compile();
        }

        private static Func<object, T> MakeInstanceGetter<T>(Type type, string name)
        {
            MemberInfo member = FindMember(type, name)
                ?? throw new MissingMemberException(type.FullName, name);

            ParameterExpression obj = Expression.Parameter(typeof(object), "obj");
            Expression typed = Expression.Convert(obj, type);
            Expression access = member is PropertyInfo p
                ? Expression.Property(typed, p)
                : Expression.Field(typed, (FieldInfo)member);

            return Expression.Lambda<Func<object, T>>(
                Expression.Convert(access, typeof(T)), obj).Compile();
        }

        private static Action<object, T> MakeInstanceSetter<T>(Type type, string name)
        {
            MemberInfo member = FindMember(type, name)
                ?? throw new MissingMemberException(type.FullName, name);

            ParameterExpression obj = Expression.Parameter(typeof(object), "obj");
            ParameterExpression val = Expression.Parameter(typeof(T), "val");
            Expression typed = Expression.Convert(obj, type);
            Expression access = member is PropertyInfo p
                ? Expression.Property(typed, p)
                : Expression.Field(typed, (FieldInfo)member);

            return Expression.Lambda<Action<object, T>>(
                Expression.Assign(access, val), obj, val).Compile();
        }
        /// <summary>
        /// 生成一个调用静态方法的委托，忽略原方法返回值。
        /// </summary>
        private static Action<TArg> MakeStaticAction<TArg>(Type type, string methodName)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic
                                     | BindingFlags.Static | BindingFlags.FlattenHierarchy;

            MethodInfo method = type.GetMethod(methodName, flags, null, new[] { typeof(TArg) }, null)
                ?? throw new MissingMethodException(type.FullName, methodName);

            ParameterExpression arg = Expression.Parameter(typeof(TArg), "arg");
            Expression call = Expression.Call(method, arg);

            //如果方法返回非 void，用 Block 把结果丢弃
            if (method.ReturnType != typeof(void))
                call = Expression.Block(call, Expression.Empty());

            return Expression.Lambda<Action<TArg>>(call, arg).Compile();
        }

    }
    /// <summary>
    /// 因为这个模组的菜单页面把tModLoader自己的菜单管理完全干掉了，而tModLoader不知道为什么
    /// <br>偏偏把切换模组菜单的 <c>MenuLoader.OffsetModMenu</c> 标成了非公开不给用。</br>
    /// <br>本来还能靠 Krafs.Publicizer 把它全部扒出来直接用，但到了现在（2026-10-5）这条路也已经走不通了。</br>
    /// <br>所以只能被迫全程走反射，并且提前在静态构造里把方法编译成委托，免得每帧都得重新 Invoke。</br>
    /// </summary>
    public class SwitchMenu : YardHud
    {
        public string TextValue;
        public float OverallSacle = 1f;
        public static float HoverLerp = 0;
        public override void PostUpdate()
        {
            /*这一段为使用krafs的情况下的原本的代码，暂时保留了
            ModMenu currentMenu = MenuLoader.CurrentMenu;
            int newMenus;
            lock (MenuLoader.menus)
            {
                string[] knownMenus = MenuLoader.KnownMenus;
                foreach (ModMenu menu in MenuLoader.menus)
                {
                    menu.IsNew = menu.IsAvailable && !knownMenus.Contains(menu.FullName);
                }
                newMenus = MenuLoader.menus.Count((ModMenu m) => m.IsNew);
            }
            Position = new Vector2(Main.screenWidth / 2, Main.screenHeight - 20);
            DynamicSpriteFont font = YardFonts.Font_YaHei.Value;
            TextValue = $"{Language.GetTextValue("tModLoader.ModMenuSwap")}: {currentMenu.DisplayName}{(newMenus == 0 ? "" : ModLoader.notifyNewMainMenuThemes ? $" ({newMenus} New)" : "")}";
            Vector2 size = ChatManager.GetStringSize(font, ChatManager.ParseMessage(TextValue, DrawColor).ToArray(), Vector2.One);
            Rectangle = Utils.CenteredRectangle(Position, size * .65f);
            */
            object currentMenu = MenuReflect.CurrentMenu();
            IList menus = MenuReflect.Menus();
            string[] knownMenus = MenuReflect.KnownMenus();

            int newMenus = 0;
            lock (menus)
            {
                foreach (object menu in menus)
                {
                    bool isNew = MenuReflect.GetIsAvailable(menu)
                              && !knownMenus.Contains(MenuReflect.GetFullName(menu));
                    MenuReflect.SetIsNew(menu, isNew);
                    if (isNew) newMenus++;
                }
            }

            Position = new Vector2(Main.screenWidth * 0.5f, Main.screenHeight - 20f);

            DynamicSpriteFont font = YardFonts.Font_YaHei.Value;
            string displayName = MenuReflect.GetDisplayName(currentMenu);

            string newSuffix = newMenus > 0 && MenuReflect.NotifyNewThemes()
                ? $" ({newMenus} New)"
                : "";

            TextValue = $"{Language.GetTextValue("tModLoader.ModMenuSwap")}: {displayName}{newSuffix}";

            Vector2 size = ChatManager.GetStringSize(
                font,
                ChatManager.ParseMessage(TextValue, DrawColor).ToArray(),
                Vector2.One);

            Rectangle = Utils.CenteredRectangle(Position, size * .65f);
        }
        public override void StartHover()
        {
        }
        public override void MouseHover(bool isHover)
        {
            if (isHover)
            {
                HoverLerp = Lerp(HoverLerp, 1f, .2f);
                if (Main.mouseLeft || Main.mouseRight)
                    Scale2 = Lerp(Scale2, 0.95f, 0.2f);
                else
                    Scale2 = Lerp(Scale2, 1.05f, 0.2f);
            }
            else
            {
                Scale2 = Lerp(Scale2, 1f, 0.2f);
                HoverLerp = Lerp(HoverLerp, 0, .2f);
                if (HoverLerp <= .02f)
                    HoverLerp = 0;
            }
        }
        public override void OnMouseLeftRelease()
        {
            if (YardMenu.CanSwitchToOtherMenu)
            {
                SoundEngine.PlaySound(YardModSounds.MenuPress);
                MenuReflect.OffsetModMenu(1);
            }
        }
        public override void OnMouseRightRelease()
        {
            if (YardMenu.CanSwitchToOtherMenu)
            {
                SoundEngine.PlaySound(YardModSounds.MenuPress);
                MenuReflect.OffsetModMenu(-1);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //这个Ratios是从0到1的，因此这里也是逆向的
            //float logoRatios = Logo.LogoScaleRatios;
            DynamicSpriteFont font = YardFonts.Font_YaHei.Value;
            Texture2D backgroundPlate = YardModAssets.CGsButtonSelected.Value;
            Vector2 plateOrigin = new Vector2(backgroundPlate.Size().X / 2, backgroundPlate.Size().Y / 2);
            Vector2 platePos = Position;
            Vector2 plateSize = new Vector2(1, 1.1f);
            Vector2 textSize = ChatManager.GetStringSize(font, TextValue, Vector2.One);
            //目标宽度 = 文本宽度 + 左右各 padding 像素的留白
            float desiredWidth = textSize.X * .6f;

            //用纹理原始宽度计算缩放，使绘制宽度恰好等于 desiredWidth
            float plateScaleX = desiredWidth / backgroundPlate.Width;
            Vector2 plateScale = new Vector2(plateScaleX, 1);
            spriteBatch.Draw(backgroundPlate, platePos, null, Color.White * HoverLerp, 0, plateOrigin, plateScale * Scale2, 0, 0);
            spriteBatch.Draw(backgroundPlate, platePos, null, Color.White * HoverLerp, Pi, plateOrigin, plateScale * Scale2, 0, 0);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            for (int i = 0; i < 8; i++)
                ChatManager.DrawColorCodedString(spriteBatch, font, TextValue, Position + (TwoPi / 8f * i).ToRotationVector2() * 1.015f, Color.White * .25f, 0, textSize / 2, Vector2.One * Scale2 * .55f);
            ChatManager.DrawColorCodedString(spriteBatch, font, TextValue, Position, Color.White, 0, textSize / 2, Vector2.One * Scale2 * .55f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);

        }
    }
}
