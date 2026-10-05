using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Menus.Managers;

namespace YogsothothsYardMod.Menus.AltMenu
{
    public class Achievements : Credits
    {
        public override int Count => 2;
        public static new float LerpScale = 1f;
        public override int TargetMenuID => MenuID.FancyUI;
        public override void OnMouseLeftRelease()
        {
            if (YardMenuUpdates.BanSwitchMenu)
                return;
            YardMenuMethods.OpenAchievements();
            YardMenuDraw.DrawTextValue = TextValue;
            YardMenuDraw.IsOpeningAchievement = true;
            SoundEngine.PlaySound(YardModSounds.MenuPress);
            YardMenuLayers.OverlayBlackOpacity = 0;
            YardMenuUpdates.GeneralFadingRatios = 0;
        }
        public override void MouseHover(bool isHover)
        {
            MouseIsHovering = isHover;
            if (isHover)
            {
                if (Main.mouseLeft)
                    LerpScale = Lerp(LerpScale, 0.9f, .2f);
                else
                    LerpScale = Lerp(LerpScale, 1.15f, .2f);
            }
            else
                LerpScale = Lerp(LerpScale, 1f, .2f);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D tex = MouseIsHovering ? TextureHovering : NoTextureHovering;
            spriteBatch.Draw(tex, ButtonPosition, null, Color.White * GetInstance<CGsHud>().Opacity, 0, tex.Size() / 2f, LerpScale, 0, 0);
            DynamicSpriteFont dynamicSpriteFont = YardFonts.Font_YaHei.Value;
            Vector2 scale = new Vector2(1f);
            Vector2 scale2 = new Vector2(1f);
            LocalizedText path = Language.GetOrRegister("Mods.YogsothothsYardMod.Menu." + TextKeyName);
            string value = path.Value;
            Vector2 textPos = Position + Vector2.UnitY;
            ChatManager.DrawColorCodedString(spriteBatch, dynamicSpriteFont, value,
                textPos, Color.White * GetInstance<CGsHud>().Opacity, 0, dynamicSpriteFont.MeasureString(value) / 2f, Vector2.One * .55f * LerpScale);


        }
    }
}
