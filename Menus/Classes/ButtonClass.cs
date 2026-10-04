using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Steamworks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.Huds;
using YogsothothsYardMod.Menus.Managers;

namespace YogsothothsYardMod.Menus.Classes
{
    public abstract class YardButtonClass : YardHud
    {
        public virtual Texture2D NoTextureHovering => YardModAssets.InvisAsset.Value;
        /// <summary>
        /// 默认的按钮鼠标悬浮时的背景显示
        /// <br>贴图材质默认为庭院主界面的那个东西</br>
        /// </summary>
        public virtual Texture2D TextureHovering => YardModAssets.ButtonBackgroundMain.Value;
        /// <summary>
        /// 文本的路径名
        /// <br>填入的是对应的Localization的键，在基类里会自动管理</br>
        /// </summary>
        public virtual string TextKeyName => GetType().Name;
        public virtual string HardcodeName => TextKeyName;
        /// <summary>
        /// 按钮的原点。默认00
        /// </summary>
        public virtual Vector2 Origin => Vector2.Zero;
        /// <summary>
        /// 该按钮导向的下一个Menu的名字
        /// </summary>
        public virtual int TargetMenuID => MenuID.CharacterSelect;
        /// <summary>
        /// 按钮的碰撞箱
        /// </summary>
        public virtual Rectangle Hitbox => Utils.CenteredRectangle(Position, new(180, 60));
        /// <summary>
        /// 按钮的大小，只作用于背景图
        /// </summary>
        public virtual float ButtonScale => 1f;
        public static float LerpButtonScale = 1;
        public virtual Vector2 ButtonPosition => Vector2.Zero;
        public virtual Color TextColor => Color.White;
        public virtual void FinalPostUpdate() { }
        public bool MouseIsHovering = false;
        public override void SetDefaults()
        {
            Position = ButtonPosition;
            Rectangle = Hitbox;
            Opacity = 1;
        }
        public override void StartHover()
        {
            base.StartHover();
        }
        public override void MouseHover(bool isHover)
        {
            MouseIsHovering = isHover;
        }
        public override void MouseLeft()
        {
            base.MouseLeft();
        }
        public override void OnMouseLeftRelease()
        {
            YardMenuMethods.ChangeMenu(TargetMenuID);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawIconHandler(spriteBatch);

        }
        public void DrawIconHandler(SpriteBatch spriteBatch, float lerpingScale = 1f, float lerpingOpcaity = 1f)
        {
            Texture2D backgroundTexture = MouseIsHovering ? TextureHovering : NoTextureHovering;
            float lerpY = Lerp(75f, 0f, lerpingOpcaity);
            //背景
            if (lerpingOpcaity >= .98f)
                spriteBatch.Draw(backgroundTexture, Position - Vector2.UnitY * 10f, null, Color.White, 0, backgroundTexture.Size() / 2f, ButtonScale * new Vector2(1, 2.5f), 0, 0);
            //字体
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);

            DynamicSpriteFont dynamicSpriteFont = YardFonts.Font_YaHei.Value;
            Vector2 scale = new Vector2(1f);
            Vector2 scale2 = new Vector2(1f);
            LocalizedText path = Language.GetOrRegister("Mods.YogsothothsYardMod.Menu." + TextKeyName);
            string value = path.Value;
            string value2 = HardcodeName;
            Vector2 textPos = Position + Vector2.UnitY * lerpY;
            Vector2 textPos2 = Position - Vector2.UnitY * 25f + Vector2.UnitY* lerpY;
            ChatManager.DrawColorCodedString(spriteBatch, dynamicSpriteFont, value2,
                textPos2, Color.White * lerpingOpcaity, 0, dynamicSpriteFont.MeasureString(value2) / 2f, Vector2.One * 1.05f * lerpingScale);
            ChatManager.DrawColorCodedString(spriteBatch, dynamicSpriteFont, value,
                textPos, Color.White * lerpingOpcaity, 0, dynamicSpriteFont.MeasureString(value) / 2f, Vector2.One * .55f * lerpingScale);
            //spriteBatch.DrawCube(Rectangle);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
        }
        public override void PostUpdate()
        {
            Vector2 velOffset = Vector2.UnitX * 15f + Vector2.UnitY * 20f;
            Position = ButtonPosition + velOffset;
            Rectangle = Utils.CenteredRectangle(Position - Vector2.UnitY * 10, new(180, 60));
            FinalPostUpdate();
        }
    }
}
