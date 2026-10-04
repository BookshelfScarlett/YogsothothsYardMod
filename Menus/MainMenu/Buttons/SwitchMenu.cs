using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.Huds;
using YogsothothsYardMod.Menus.Managers;

namespace YogsothothsYardMod.Menus.MainMenu.Buttons
{
    public class SwitchMenu : YardHud
    {
        public string TextValue;
        public override void PostUpdate()
        {
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
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            TextValue = $"{Language.GetTextValue("tModLoader.ModMenuSwap")}: {currentMenu.DisplayName}{(newMenus == 0 ? "" : ModLoader.notifyNewMainMenuThemes ? $" ({newMenus} New)" : "")}";
            Vector2 size = ChatManager.GetStringSize(font, ChatManager.ParseMessage(TextValue, DrawColor).ToArray(), Vector2.One);
            Rectangle = Utils.CenteredRectangle(Position, size);
        }
        public override void StartHover()
        {
        }
        public override void MouseHover(bool isHover)
        {
            if (isHover)
            {
                if (Main.mouseLeft || Main.mouseRight)
                    Scale2 = Lerp(Scale2, 0.95f, 0.2f);
                else
                    Scale2 = Lerp(Scale2, 1.05f, 0.2f);
            }
            else
            {
                Scale2 = Lerp(Scale2, 1f, 0.2f);
            }
        }
        public override void OnMouseLeftRelease()
        {
            if (YardMenu.CanSwitchToOtherMenu)
            {
                SoundEngine.PlaySound(YardModSounds.MenuPress);
                MenuLoader.OffsetModMenu(1);
            }
        }
        public override void OnMouseRightRelease()
        {
            if (YardMenu.CanSwitchToOtherMenu)
            {
                SoundEngine.PlaySound(YardModSounds.MenuPress);
                MenuLoader.OffsetModMenu(-1);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //这个Ratios是从0到1的，因此这里也是逆向的
            float ratios = (1);
            //float logoRatios = Logo.LogoScaleRatios;
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            Vector2 size = ChatManager.GetStringSize(font, TextValue, Vector2.One);
            ChatManager.DrawColorCodedString(spriteBatch, font, TextValue, Position, Color.Silver * ratios, 0, size / 2, Vector2.One * Scale2);

            //在这里画模组的版本字号。

        }
    }
}
