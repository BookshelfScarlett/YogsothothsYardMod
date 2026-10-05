using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Menus.Classes;

namespace YogsothothsYardMod.Menus.MainMenu.Buttons
{
    /// <summary>
    /// 退出界面
    /// </summary>
    public class Exit : YardButtonClass
    {
        public override Vector2 ButtonPosition => YardMethods.GetScreenSize / 2f - new Vector2(400, -325);
        public override int TargetMenuID => MenuID.CharacterSelect;
        public override float ButtonScale => base.ButtonScale;
        public override Rectangle Hitbox => Utils.CenteredRectangle(Position, new Vector2(300, 50));
        public override string TextKeyName => "Exit";
        public static float LerpScaleValue = 1f;
        public static float LerpOpacityValue = 1;
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

        public override void OnMouseLeftRelease()
        {
            //不做2级ui跳转了，直接退出游戏，符合泰拉瑞亚的游戏直觉
            SoundEngine.PlaySound(YardModSounds.MenuPress);
            Main.instance.Exit();
        }
        public override void FinalPostUpdate()
        {
            if (Options.LerpOpacityValue >= .95f)
            {
                if (LerpOpacityValue >= 1f)
                    return;
                LerpOpacityValue = Lerp(LerpOpacityValue, 1f, .12f);
                if (LerpOpacityValue >= .98f)
                    LerpOpacityValue = 1f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawIconHandler(spriteBatch, LerpScaleValue, LerpOpacityValue);
        }
    }
}
