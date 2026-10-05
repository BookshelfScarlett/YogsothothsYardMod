using System.ComponentModel;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace YogsothothsYardMod.Globals.Configs
{
    public class YardModClientConfig : ModConfig, ILocalizedModType
    {
        public static YardModClientConfig Instance;
        public override void OnLoaded()
        {
            Instance = this;
        }
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message) => false;

        [BackgroundColor(139, 0, 0, 192)]
        [DefaultValue(true)]
        public bool DrawIcon { get; set; }

        [BackgroundColor(139, 0, 0, 192)]
        [DefaultValue(true)]
        public bool DrawExecutionCounter { get; set; }

        [BackgroundColor(139, 0, 0, 192)]
        [Range(0, 10f)]
        [DefaultValue(1f)]
        public float ScreenShakeStrength { get; set; }

        [BackgroundColor(139, 0, 0, 192)]
        [Range(0f, 1f)]
        [DefaultValue(1f)]
        public float ScreenDarkStrength { get; set; }
        [BackgroundColor(139, 0, 0, 192)]
        [Range(0f, 1f)]
        [DefaultValue(1f)]
        public float TextboxSize { get; set; }
    }
}
