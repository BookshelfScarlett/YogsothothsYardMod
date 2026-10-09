using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace YogsothothsYardMod.Globals.Configs
{
    public class YardServerConfig : ModConfig
    {
        public static YardServerConfig Instance;
        public override void OnLoaded()
        {
            Instance = this;
        }
        public override ConfigScope Mode => ConfigScope.ServerSide;
        [BackgroundColor(211, 211, 211, 192)]
        [Range(0, 50f)]
        [DefaultValue(1f)]
        public float ModWeaponDamageMult { get; set; }
    }
}
