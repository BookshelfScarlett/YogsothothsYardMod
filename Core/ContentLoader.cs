using YogsothothsYardMod.Core.Systems;

namespace YogsothothsYardMod.Core
{
    public static class ScarletContent
    {
        public static int DashType<T>() where T : PlayerDashClass => GetInstance<T>()?.Type ?? 0;
    }
}
