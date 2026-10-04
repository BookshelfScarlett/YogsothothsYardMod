using Microsoft.Xna.Framework.Graphics;
using Terraria;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.Huds;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Menus.MainMenu;
using YogsothothsYardMod.Menus.MainMenu.Buttons;

namespace YogsothothsYardMod.Menus.Managers
{
    public class YardMenuDraw
    {
        public static YardHud LoadGame => YardHudManager.UICollection[GetInstance<LoadGames>().Type];
        public static YardHud Multiplayer => YardHudManager.UICollection[GetInstance<Multiplayer>().Type];
        public static YardHud Options => YardHudManager.UICollection[GetInstance<Options>().Type];
        public static YardHud Exit => YardHudManager.UICollection[GetInstance<Exit>().Type];
        public static YardHud Workshop => YardHudManager.UICollection[GetInstance<Workshop>().Type];
        public static YardHud CGs => YardHudManager.UICollection[GetInstance<CGs>().Type];
        public static YardHud SwitchMenu => YardHudManager.UICollection[GetInstance<SwitchMenu>().Type];
        public static YardHud JumpToB2 => YardHudManager.UICollection[GetInstance<JumpToB2>().Type];
        public static YardHud JumpToDC => YardHudManager.UICollection[GetInstance<JumpToDC>().Type];
        public static YardHud JumpToSina => YardHudManager.UICollection[GetInstance<JumpToSina>().Type];
        public static YardHud JumpToX => YardHudManager.UICollection[GetInstance<JumpToX>().Type];

        public static SpriteBatch SB { get => Main.spriteBatch; }
        public static void PreDraw()
        {
            YardMethods.EnterHudArea(BlendState.NonPremultiplied, SamplerState.LinearClamp);
            YardMenuBackground.DrawBackground();
            DrawButton();
            //退出游戏
            SB.End();
            SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
        }
        public static string DrawTextValue = "None";
        public static void PostDraw()
        {
            if (Main.menuMode != YardMenu.ID)
            {
                YardMethods.EnterHudArea(BlendState.NonPremultiplied, SamplerState.LinearClamp);
                Vector2 screenSize = YardMethods.GetScreenSize;
                Rectangle rec = Utils.CenteredRectangle(screenSize / 2f, new Vector2(screenSize.X, screenSize.Y));
                SB.Draw(YardModAssets.Texture_WhiteCubeBig.Value, rec, Color.Black * .85f);
                float xBlock = screenSize.X * .45f;
                float yBlock = screenSize.Y+10;
                rec = Utils.CenteredRectangle(screenSize / 2f, new Vector2(xBlock, yBlock));
                SB.Draw(YardModAssets.Texture_WhiteCubeBig.Value, rec, Color.Lerp(Color.DarkRed, Color.Black, .5f));
                rec = Utils.CenteredRectangle(screenSize / 2f, new Vector2(xBlock * .99f, yBlock * .99f));
                Texture2D tex = YardMenu.GetBlackRedGradient(Main.graphics.GraphicsDevice);
                SB.Draw(tex, rec, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                SB.End();
                SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            }
        }
        private static void DrawButton()
        {
            LoadGame.Draw(SB);
            Workshop.Draw(SB);
            Options.Draw(SB);
            SwitchMenu.Draw(SB);
            Multiplayer.Draw(SB);
            Exit.Draw(SB);
            CGs.Draw(SB);
            JumpToX.Draw(SB);
            JumpToSina.Draw(SB);
            JumpToDC.Draw(SB);
            JumpToB2.Draw(SB);
        }
    }
}
