using Microsoft.Xna.Framework.Graphics;
using Terraria;
using YogsothothsYardMod.Assets.Register;

namespace YogsothothsYardMod.Menus.MainMenu.Buttons
{
    public class JumpToX : JumpToB2
    {
        public override Texture2D IconTex => YardModAssets.IconX.Value;
        public override string WebLine => "https://x.com/BoneNailStudio?s=20";
        public override int WebCount => 3;
        public override float TextOffsetX => base.TextOffsetX;
        public override float BackgroundScaleX => base.BackgroundScaleX;
        public static new float PosOffsetY = 1;
        public override void PostUpdate()
        {
            Position = IconCenter;
            Rectangle = Utils.CenteredRectangle(Position, IconTex.Size() * .32f);
            if (JumpToDC.PosOffsetY >= .8f)
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
            DrawHandler(spriteBatch,PosOffsetY);
        }
    }
}
