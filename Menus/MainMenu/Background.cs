using Microsoft.Xna.Framework.Graphics;
using Terraria;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Menus.MainMenu
{
    public class YardMenuBackground
    {

        public static bool CanChangeMenu = false;
        public static float TheScaleRatios = 0;
        public static void Update()
        {
            TheScaleRatios = Lerp(TheScaleRatios, 1f, .04f);
            if (TheScaleRatios > .98f)
                TheScaleRatios = 1f;
        }
        public static SpriteBatch SB { get => Main.spriteBatch; }
        public static void DrawBackground()
        {
            Texture2D tex = YardModAssets.BackgroundMain.Value;
            float scaleRatios;
            float targetValue = 0.835f;
            float beginValue = 0.85f;
            scaleRatios = Clamp(Lerp(beginValue, targetValue, TheScaleRatios), targetValue, beginValue);
            SB.Draw(tex, YardMethods.GetScreenSize / 2f, null, Color.White, 0, tex.Size() / 2f, scaleRatios, 0, 0);
            //我不太确定他没有遮罩。但先放在这。
        }
    }
}
