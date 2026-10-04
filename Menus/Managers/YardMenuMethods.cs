using Terraria;
using Terraria.GameContent.UI.States;
using Terraria.ID;

namespace YogsothothsYardMod.Menus.Managers
{
    public static class YardMenuMethods
    {
        public static void ChangeMenu(int TargetMenuID)
        {
            YardMenuUpdates.ToOtherMenu = true;
            YardMenuUpdates.NextMenuID = TargetMenuID;
        }
        public static void OpenWorkshop()
        {
            YardMenuUpdates.ToOtherMenu = true;
            YardMenuUpdates.NextMenuID = MenuID.FancyUI;
            YardMenuUpdates.OnChangeToTargetMenuID.Add(delegate
            {
                UIWorkshopHub workshopHub = new UIWorkshopHub(null);
                workshopHub.EnterHub();
                Main.MenuUI.SetState(workshopHub);
            });
        }
        public static void OpenAchievements()
        {
            YardMenuUpdates.ToOtherMenu = true;
            YardMenuUpdates.NextMenuID = MenuID.FancyUI;
            YardMenuUpdates.OnChangeToTargetMenuID.Add(delegate
            {
                Main.menuMode = MenuID.FancyUI;
                Main.MenuUI.SetState(Main.AchievementsMenu);
            });
        }
    }
}
