//using Microsoft.Xna.Framework.Graphics;
//using Terraria;
//using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace YogsothothsYardMod.Menus.Managers
{
    public class YardMenuLayers : ModSystem
    {
        public static float OverlayBlackOpacity = 0;
        public override void Load()
        {
            On_Main.DrawMenu += PostDrawMenu;
            On_Main.DrawVersionNumber += On_Main_DrawVersionNumber;
            On_Main.DrawSocialMediaButtons += On_Main_DrawSocialMediaButtons;
            On_Main.DrawtModLoaderSocialMediaButtons += On_Main_DrawtModLoaderSocialMediaButtons;
            On_Main.HandleNews += On_Main_HandleNews;
        }

        public static void On_Main_HandleNews(On_Main.orig_HandleNews orig, Color menuColor)
        {
            if (MenuLoader.CurrentMenu is YardMenu)
            {

            }
            else
                orig(menuColor);
        }

        public static void On_Main_DrawtModLoaderSocialMediaButtons(On_Main.orig_DrawtModLoaderSocialMediaButtons orig, Color menuColor, float upBump)
        {
            if (MenuLoader.CurrentMenu is YardMenu)
            {

            }
            else
                orig(menuColor, upBump);

        }

        public static void On_Main_DrawSocialMediaButtons(On_Main.orig_DrawSocialMediaButtons orig, Color menuColor, float upBump)
        {
            if (MenuLoader.CurrentMenu is YardMenu)
            {

            }
            else
                orig(menuColor, upBump);

        }

        public static void On_Main_DrawVersionNumber(On_Main.orig_DrawVersionNumber orig, Color menuColor, float upBump)
        {
            if (MenuLoader.CurrentMenu is YardMenu)
            {

            }
            else
                orig(menuColor, upBump);

        }

        public static void PostDrawMenu(On_Main.orig_DrawMenu orig, Main self, GameTime gameTime)
        {
            orig(self, gameTime);
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            OverlayBlackOpacity = Clamp(OverlayBlackOpacity, 0f, 1f);
            if (Main.menuMode != MenuID.Title && OverlayBlackOpacity > 0.02f)
            {
                Main.spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth * 2, Main.screenHeight * 2), Color.Black * OverlayBlackOpacity);
            }
            Main.spriteBatch.End();
        }
    }
}
