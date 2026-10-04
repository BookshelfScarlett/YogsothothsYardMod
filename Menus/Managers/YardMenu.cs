using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Menus.MainMenu;

namespace YogsothothsYardMod.Menus.Managers
{
    public class YardMenu : ModMenu
    {
        /// <summary>
        /// 犹格索托斯的庭院的主界面ID
        /// <br>使用的是游戏发布的日期</br>
        /// <br>我是不太相信会有人和我这个主界面一样……</br>
        /// </summary>
        public static int ID = 20231020;
        /// <summary>
        /// 把原本默认的贴图全部屏蔽掉
        /// </summary>
        public override Asset<Texture2D> SunTexture => YardModAssets.InvisAsset.Texture;
        public override Asset<Texture2D> MoonTexture => YardModAssets.InvisAsset.Texture;
        public override Asset<Texture2D> Logo => YardModAssets.InvisAsset.Texture;
        public override ModSurfaceBackgroundStyle MenuBackgroundStyle => null;
        public override void Load()
        {

            base.Load();
        }
        public override void Unload()
        {
            base.Unload();
        }
        public override int Music => MusicLoader.GetMusicSlot(YardMusic.Menu);
        public override string DisplayName => Mod.GetLocalizationKey("Menu.Name").ToLangValue();
        public static bool CanSwitchToOtherMenu;
        public static Texture2D BlackRedGradient;

        /// <summary>
        /// 生成 1×N 的垂直渐变纹理（顶部黑色 → 底部暗红）。
        /// 只需在 Load 时生成一次，后续复用。
        /// </summary>
        public static Texture2D GetBlackRedGradient(GraphicsDevice gd, int height = 256)
        {
            if (BlackRedGradient != null && !BlackRedGradient.IsDisposed)
                return BlackRedGradient;

            int begin = height / 2;
            BlackRedGradient = new Texture2D(gd, 1, height);
            Color[] data = new Color[height];
            Color top = Color.Black;
            Color bottom = Color.Lerp(Color.DarkRed, Color.Black, .75f);
            for (int i = 1; i < height; i++)
            {
                float t = i / (float)(height - 1);
                data[i] = Color.Lerp(top, bottom, t);
            }
            BlackRedGradient.SetData(data);
            return BlackRedGradient;
        }

        public override void OnSelected()
        {
            Main.menuMode = ID;
            GetBlackRedGradient(Main.graphics.GraphicsDevice);
            YardMenuBackground.TheScaleRatios = 0;
            YardMenuUpdates.GeneralFadingRatios = 0;
            YardMenuLayers.OverlayBlackOpacity = 1;
            MainMenu.Buttons.LoadGames.LerpOpacityValue = 0;
            MainMenu.Buttons.Workshop.LerpOpacityValue = 0;
            MainMenu.Buttons.Multiplayer.LerpOpacityValue = 0;
            MainMenu.Buttons.CGs.LerpOpacityValue = 0;
            MainMenu.Buttons.Options.LerpOpacityValue = 0;
            MainMenu.Buttons.Exit.LerpOpacityValue = 0;
            MainMenu.Buttons.JumpToB2.PosOffsetY = 0;
            MainMenu.Buttons.JumpToDC.PosOffsetY = 0;
            MainMenu.Buttons.JumpToSina.PosOffsetY = 0;
            MainMenu.Buttons.JumpToX.PosOffsetY = 0;
            CanSwitchToOtherMenu = false;
        }
        public override void OnDeselected()
        {
            CanSwitchToOtherMenu = false;
            if (Main.menuMode != MenuID.FancyUI)
                Main.menuMode = MenuID.Title;
            BlackRedGradient = null;

        }
        public override void Update(bool isOnTitleScreen)
        {
            if (Main.mouseLeftRelease && Main.mouseRightRelease)
                CanSwitchToOtherMenu = true;
            YardMenuUpdates.UpdatesYardMenu();
        }
        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            YardMenuDraw.PreDraw();
            return false;
        }
        public override void PostDrawLogo(SpriteBatch spriteBatch, Vector2 logoDrawCenter, float logoRotation, float logoScale, Color drawColor)
        {
            YardMenuDraw.PostDraw();
        }
    }
}
