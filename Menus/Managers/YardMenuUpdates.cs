using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using YogsothothsYardMod.Core.Huds;
using YogsothothsYardMod.Menus.AltMenu;
using YogsothothsYardMod.Menus.MainMenu;
using YogsothothsYardMod.Menus.MainMenu.Buttons;

namespace YogsothothsYardMod.Menus.Managers
{
    public class YardMenuUpdates
    {
        //这里也同样是会做一些处理的。
        public static YardHud LoadGame => YardHudManager.UICollection[GetInstance<LoadGames>().Type];
        public static YardHud Options => YardHudManager.UICollection[GetInstance<Options>().Type];
        public static YardHud Multiplayer => YardHudManager.UICollection[GetInstance<Multiplayer>().Type];
        public static YardHud Exit => YardHudManager.UICollection[GetInstance<Exit>().Type];
        public static YardHud Workshop => YardHudManager.UICollection[GetInstance<Workshop>().Type];
        public static YardHud SwitchMenu => YardHudManager.UICollection[GetInstance<SwitchMenu>().Type];
        public static YardHud CGs => YardHudManager.UICollection[GetInstance<CGs>().Type];
        public static YardHud CGsHud => YardHudManager.UICollection[GetInstance<CGsHud>().Type];
        public static YardHud JumpToB2 => YardHudManager.UICollection[GetInstance<JumpToB2>().Type];
        public static YardHud JumpToDC => YardHudManager.UICollection[GetInstance<JumpToDC>().Type];
        public static YardHud JumpToSina => YardHudManager.UICollection[GetInstance<JumpToSina>().Type];
        public static YardHud JumpToX => YardHudManager.UICollection[GetInstance<JumpToX>().Type];
        public static bool ToYardMenu = true;
        public static bool ToOtherMenu = false;
        public static bool BanSwitchMenu = false;
        public static int LastMenuID = -1;
        public static int NextMenuID = -1;
        /// <summary>
        /// 主界面按钮的总控大小
        /// </summary>
        public static float ButtonsHoverOut = 1;

        /// <summary>
        /// 我也不知道
        /// </summary>
        public static List<Action> OnChangeToTargetMenuID = [];
        /// <summary>
        /// 总的渐变，不过这个要重构了
        /// </summary>
        public static float GeneralFadingRatios = 0f;
        public static void UpdatesYardMenu()
        {
            if (Main.menuMode == YardMenu.ID)
                UpdateButtons();
            UpdateGeneral();
        }

        public static void UpdateGeneral()
        {
            //Clamp掉这个ratios
            GeneralFadingRatios = Clamp(GeneralFadingRatios, 0, 1);
            float frames = 0.22f;
            ref float BlackLayer = ref YardMenuLayers.OverlayBlackOpacity;
            //BanSwitchMenu会随时更新，确保不会出现意外
            BanSwitchMenu = BlackLayer > 0.01f;
            //只会覆盖原版主界面的时候展现背景
            //因为原版会优先切换到主界面，随后拦截主界面并标记已经切换到了自定义UI
            if (Main.menuMode == MenuID.Title)
                ToYardMenu = true;
            else
                LastMenuID = Main.menuMode;
            //从原版主界面至需要的魔裁界面，先不切换菜单，等淡出完成后再切换到主界面
            if (ToYardMenu)
            {
                if (Main.menuMode != LastMenuID)
                    Main.menuMode = LastMenuID;
                YardMenuDraw.IsOpeningAchievement = false;
                GeneralFadingRatios = Lerp(GeneralFadingRatios, 1f, frames);
                if (BlackLayer < 1f)
                    BlackLayer = Lerp(BlackLayer, 1f, frames);
                if (GeneralFadingRatios > 0.98f)
                {
                    GeneralFadingRatios = 1f;
                    BlackLayer = 1f;
                    ToYardMenu = false;
                    Main.menuMode = YardMenu.ID;
                }
            }
            else if (ToOtherMenu)
            {
                //切换至其他界面
                //原游戏由主界面到其他界面没有渐变，我们这也不会做渐变。
                //我草他妈有
                GeneralFadingRatios = Lerp(GeneralFadingRatios, 1f, frames);
                BlackLayer = Lerp(BlackLayer, 1f, frames);
                YardMenuBackground.Update();
                if (GeneralFadingRatios >= 0.98f)
                {
                    Main.menuMode = NextMenuID;
                    //设定为1，因为仍然需要绘制背景
                    GeneralFadingRatios = 1;
                    BlackLayer = 1f;
                    NextMenuID = -1;
                    ToOtherMenu = false;
                    if (OnChangeToTargetMenuID.Count != 0)
                    {
                        for (int i = 0; i < OnChangeToTargetMenuID.Count; i++)
                        {
                            Action action = OnChangeToTargetMenuID[i];
                            action();
                        }
                        OnChangeToTargetMenuID.Clear();
                    }
                }
            }
            else //默认会慢慢淡出
            {
                YardMenuBackground.Update();
                //第一次加载魔裁主界面时，我们才用非常慢的缓动
                BlackLayer = Lerp(BlackLayer, 0f, frames);
            }
        }
        public static void UpdateButtons()
        {
            LoadGame.Update();
            Options.Update();
            Workshop.Update();
            SwitchMenu.Update();
            Multiplayer.Update();
            CGs.Update();
            Exit.Update();
            JumpToB2.Update();
            JumpToDC.Update();
            JumpToSina.Update();
            JumpToX.Update();
            CGsHud.Update();
        }
    }
}
