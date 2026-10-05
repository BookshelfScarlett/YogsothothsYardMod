using Terraria.ModLoader;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Globals.Players
{
    public partial class YardPlayer : ModPlayer
    {
        public override void PreUpdate()
        {
            if(infiniteFlightTime)
                Player.wingTime = Player.wingTimeMax;
        }
    }
}
