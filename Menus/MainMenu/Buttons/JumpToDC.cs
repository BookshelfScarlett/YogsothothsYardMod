using Microsoft.Xna.Framework.Graphics;
using Terraria;
using YogsothothsYardMod.Assets.Register;

namespace YogsothothsYardMod.Menus.MainMenu.Buttons
{
    public class JumpToDC : JumpToB2
    {
        public override Texture2D IconTex => YardModAssets.IconDiscord.Value;
        public override string WebLine => "https://discord.com/invite/tcq4ERyUxK";
        public override int WebCount => 4;
        public override float TextOffsetX => base.TextOffsetX;
        public override float BackgroundScaleX => 2;
        public static new float PosOffsetY = 1;
        public override void PostUpdate()
        {
            Position = IconCenter;
            Rectangle = Utils.CenteredRectangle(Position, IconTex.Size() * .32f);
            if (Exit.LerpOpacityValue >= .8f)
            {
                if (PosOffsetY >= 1f)
                    return;
                PosOffsetY = Lerp(PosOffsetY, 1f, .12f);
                if (PosOffsetY >= .98f)
                    PosOffsetY = 1f;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawHandler(spriteBatch, PosOffsetY);
        }
    }
}
