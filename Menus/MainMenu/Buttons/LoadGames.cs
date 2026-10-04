using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using YogsothothsYardMod.Core.Huds;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Menus.Classes;
using YogsothothsYardMod.Menus.Managers;

namespace YogsothothsYardMod.Menus.MainMenu.Buttons
{
    public class LoadGames : YardButtonClass
    {
        public override Vector2 ButtonPosition => YardMethods.GetScreenSize / 2f - new Vector2(400, -25);
        public override int TargetMenuID => MenuID.None;
        public override float ButtonScale => base.ButtonScale;
        public override string HardcodeName => "Load";
        public override Rectangle Hitbox => Utils.CenteredRectangle(Position, new Vector2(300, 50));
        public static float LerpScaleValue = 1f;
        public static float LerpOpacityValue = 1;

        public override void OnMouseLeftRelease()
        {
            //需要进入alt的二级UI
            //我们需要在这里同时处理两个事项：多人游戏与单人游戏
            //创意工坊按钮已经单独解离出去作为第二按钮，
            if (!YardHudManager.ActiveDepth[2])
            {
                YardMenuUpdates.GeneralFadingRatios = 0;
            }
        }
        public override void MouseHover(bool isHover)
        {
            base.MouseHover(isHover);
            if (isHover)
            {
                if (Main.mouseLeft)
                    LerpScaleValue = Lerp(LerpScaleValue, .9f, .2f);
            }
            else
                LerpScaleValue = Lerp(LerpScaleValue, 1f, .2f);
        }
        public override void FinalPostUpdate()
        {
            if (LerpOpacityValue >= 1f)
                return;
            LerpOpacityValue = Lerp(LerpOpacityValue, 1f, .12f);
            if (LerpOpacityValue >= .98f)
                LerpOpacityValue = 1f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawIconHandler(spriteBatch, LerpScaleValue, LerpOpacityValue);
        }
    }
}
