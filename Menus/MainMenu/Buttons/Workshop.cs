using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.Database.Enums;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Menus.Classes;
using YogsothothsYardMod.Menus.Managers;

namespace YogsothothsYardMod.Menus.MainMenu.Buttons
{
    /// <summary>
    /// 创意工坊的专属按钮。
    /// </summary>
    public class Workshop : YardButtonClass
    {
        public override Vector2 ButtonPosition => YardMethods.GetScreenSize / 2f - new Vector2(400, -175);
        public override float ButtonScale => base.ButtonScale;
        public override int TargetMenuID => MenuID.FancyUI;
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
            YardMenuUpdates.GeneralFadingRatios = 0;
            YardMenuMethods.ChangeMenu(TargetMenuID);
            LocalizedText path = Language.GetOrRegister("Mods.YogsothothsYardMod.Menu." + TextKeyName);
                SoundEngine.PlaySound(YardModSounds.MenuPress);
            string value = path.Value;
            YardMenuDraw.DrawTextValue = value;
            YardWaifus randWaifu = Main.rand.NextFromList([YardWaifus.LittleLeaf, YardWaifus.Tlipoca, YardWaifus.XiaLuLing, YardWaifus.Yevna]);
            YardMenuDraw.Waifus = randWaifu;
            int f = Main.rand.NextFromList([0, 2, 4]);
            Vector2 f2 = Main.rand.NextFromList([new Vector2(600f, 150f), new Vector2(-600, -150f)]);
            YardMenuDraw.Useframe = f;
            YardMenuDraw.CurCharactorFrame = f;
            YardMenuDraw.CurPosIndex = Main.rand.Next(0, YardMenuDraw.RandPosList.Count);
            YardMenuDraw.DrawTextValue = value;

            YardMenuMethods.OpenWorkshop();
        }
        public override void FinalPostUpdate()
        {
            if (Multiplayer.LerpOpacityValue >= .95f)
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
