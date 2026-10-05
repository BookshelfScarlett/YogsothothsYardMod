using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Menus.Classes;
using YogsothothsYardMod.Menus.Managers;

namespace YogsothothsYardMod.Menus.AltMenu
{
    public class Credits : YardButtonClass
    {
        public virtual int Count => 1;
        public override Texture2D TextureHovering => YardModAssets.CGsButtonSelected.Value;
        public override Texture2D NoTextureHovering => YardModAssets.CGsButtonNotSelected.Value;
        public override int UIDepth => 2;
        public override Rectangle Hitbox => Utils.CenteredRectangle(ButtonPosition, new Vector2(300 * LerpScale, 50));
        public static float LerpScale = 1;
        public override int TargetMenuID => MenuID.CreditsRoll;
        public override bool PreSetDepth()
        {
            return false;
        }
        public override Vector2 ButtonPosition => YardMethods.GetScreenSize / 2f + Vector2.UnitY * Count * 200 - Vector2.UnitY * 300f;
        public override void OnMouseLeftRelease()
        {
            if (YardMenuUpdates.BanSwitchMenu)
                return;
            YardMenuMethods.ChangeMenu(TargetMenuID);
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

        public override void PostUpdate()
        {
            Position = ButtonPosition;
            Rectangle = Hitbox;
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
