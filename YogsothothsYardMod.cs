global using Microsoft.Xna.Framework;
global using static Microsoft.Xna.Framework.MathHelper;
global using static Terraria.ModLoader.ModContent;
global using static YogsothothsYardMod.Core.Handler.EasingHandler;
global using static YogsothothsYardMod.Core.Handler.GlobalsHandlers;
global using static YogsothothsYardMod.Core.Handler.RandHandler;
using Terraria.ModLoader;

namespace YogsothothsYardMod
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class YogsothothsYardMod : Mod
    {
        public static YogsothothsYardMod Instance;
        public override void Load()
        {
            Instance = this;
        }
        public override void Unload()
        {
            Instance = null;
        }
    }
}
