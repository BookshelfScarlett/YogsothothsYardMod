using Terraria.Audio;

namespace YogsothothsYardMod.Assets.Register
{
    public static class YardModSounds
    {
        private static string SoundsPath => "YogsothothsYardMod/Assets/Sounds/";
        public static SoundStyle Misc_ManaClearUse => new SoundStyle($"{SoundsPath}{nameof(Misc_ManaClearUse)}");
        public static SoundStyle Tlipoca_SoulAbsorb => new SoundStyle($"{SoundsPath}{nameof(Tlipoca_SoulAbsorb)}");
        public static SoundStyle Tlipoca_StoneBonk => new SoundStyle($"{SoundsPath}{nameof(Tlipoca_StoneBonk)}", numVariants: 2);
        public static SoundStyle Tlipoca_StoneShatter => new SoundStyle($"{SoundsPath}{nameof(Tlipoca_StoneShatter)}");
        public static SoundStyle Tlipoca_Swing => new SoundStyle($"{SoundsPath}{nameof(Tlipoca_Swing)}", numVariants: 2);
        public static SoundStyle Tlipoca_NpcKillSound => new SoundStyle($"{SoundsPath}{nameof(Tlipoca_NpcKillSound)}");
        public static SoundStyle GalvanizedHand_Charge => new($"{SoundsPath}{nameof(GalvanizedHand_Charge)}", numVariants: 2);
        public static SoundStyle MenuPress => new($"{SoundsPath}{nameof(MenuPress)}");
    }
}
