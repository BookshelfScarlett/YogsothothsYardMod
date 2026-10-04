using Terraria.ModLoader;

namespace YogsothothsYardMod.Assets.Register
{
    public partial class YardModAssets : ModSystem
    {
        private string TexPath => "YogsothothsYardMod/Assets";
        private string Path_Texture => $"{TexPath}/Texture/";
        private string Path_Metaball => $"{TexPath}/Texture/Metaball/";
        private string Path_General => $"{TexPath}/Texture/General/";
        private string Path_Particle => $"{TexPath}/Texture/Particle/";
        public static Tex2DWithPath Metaball_Bloody { get; private set; }
        public static Tex2DWithPath ScarletGhost { get; private set; }

        public static Tex2DWithPath InvisAsset { get; private set; }
        public static Tex2DWithPath Texture_BloomRing { get; set; }
        public static Tex2DWithPath Texture_BloomShockwave { get; set; }
        public static Tex2DWithPath Texture_SoftCircleEdge { get; set; }
        public static Tex2DWithPath Texture_WhiteCube { get; set; }
        public static Tex2DWithPath Texture_WhiteCubeBig { get; set; }
        public static Tex2DWithPath Texture_WhiteCircle { get; set; }
        public static Tex2DWithPath Texture_Spirite { get; set; }
        public static Tex2DWithPath Texture_EnergySword { get; set; }
        public static Tex2DWithPath Texture_RarityGlow { get; set; }
        public static Tex2DWithPath Texture_StandardGradient { get; set; }
        public static Tex2DWithPath Texture_SnowCloud { get; set; }
        public static Tex2DWithPath Texture_SwordSlash { get; set; }
        public static Tex2DWithPath Texture_SwordSlashWhite { get; set; }
        public static Tex2DWithPath Texture_SwordSlash1 { get; set; }
        public static Tex2DWithPath Texture_SwordSlash2 { get; set; }
        public static Tex2DWithPath Texture_Fog { get; set; }
        public static Tex2DWithPath Texture_BloodStain { get; set; }
        public static Tex2DWithPath Texture_RuShiWoWenFlower { get; set; }
        public static Tex2DWithPath Texture_FireBall { get; set; }
        public static Tex2DWithPath Texture_FireBallPixel { get; set; }

        public static Tex2DWithPath Noise_Misc { get; set; }
        public static Tex2DWithPath Noise_Misc2 { get; set; }
        public static Tex2DWithPath Noise_Aura { get; set; }
        public static Tex2DWithPath Noise_EmptyAura { get; set; }
        public static Tex2DWithPath Noise_HeavyAura { get; set; }
        public static Tex2DWithPath Noise_Smoke { get; set; }
        public static Tex2DWithPath Noise_WaterFlow { get; set; }
        public static Tex2DWithPath Noise_BlackGalaxy1 { get; set; }
        public static Tex2DWithPath Noise_BlackGalaxy2 { get; set; }

        public static Tex2DWithPath Trail_ManaStreak { get; set; }
        public static Tex2DWithPath Trail_ManaStreakTiny { get; set; }
        public static Tex2DWithPath Trail_RvSlash { get; set; }
        public static Tex2DWithPath Trail_VShapeWithTail { get; set; }
        public static Tex2DWithPath Trail_ParaLine { get; set; }
        public static Tex2DWithPath Trail_TerraRayFlow { get; set; }
        public static Tex2DWithPath Trail_FadedStreak { get; set; }
        public static Tex2DWithPath Trail_MegaBeam { get; set; }
        public static Tex2DWithPath Trail_ManaMegaBeam { get; set; }
        public static Tex2DWithPath Trail_Lightning0 { get; set; }
        public static Tex2DWithPath Trail_Lightning1 { get; set; }
        public static Tex2DWithPath Trail_Lightning2 { get; set; }
        public static Tex2DWithPath Trail_Lightning3 { get; set; }
        public static Tex2DWithPath Trail_Lightning4 { get; set; }
        public static Tex2DWithPath Trail_BloomDualLine { get; set; }
        public void LoadTexture()
        {
            InvisAsset = new Tex2DWithPath(Path_Texture + nameof(InvisAsset));
            ScarletGhost = new Tex2DWithPath(Path_Texture + nameof(ScarletGhost));

            Texture_BloomRing = new Tex2DWithPath($"{Path_General}{nameof(Texture_BloomRing)}");
            Texture_BloomShockwave = new Tex2DWithPath($"{Path_General}{nameof(Texture_BloomShockwave)}");
            Texture_SoftCircleEdge = new Tex2DWithPath($"{Path_General}{nameof(Texture_SoftCircleEdge)}");
            Texture_WhiteCube = new Tex2DWithPath($"{Path_General}{nameof(Texture_WhiteCube)}");
            Texture_WhiteCubeBig = new Tex2DWithPath($"{Path_General}{nameof(Texture_WhiteCubeBig)}");
            Texture_WhiteCircle = new Tex2DWithPath($"{Path_General}{nameof(Texture_WhiteCircle)}");
            Texture_Spirite = new Tex2DWithPath($"{Path_General}{nameof(Texture_Spirite)}");
            Texture_EnergySword = new Tex2DWithPath($"{Path_General}{nameof(Texture_EnergySword)}");
            Texture_RarityGlow = new Tex2DWithPath($"{Path_General}{nameof(Texture_RarityGlow)}");
            Texture_StandardGradient = new Tex2DWithPath($"{Path_General}{nameof(Texture_StandardGradient)}");
            Texture_SnowCloud = new Tex2DWithPath($"{Path_General}{nameof(Texture_SnowCloud)}");
            Texture_SwordSlash = new Tex2DWithPath($"{Path_General}{nameof(Texture_SwordSlash)}");
            Texture_SwordSlashWhite = new Tex2DWithPath($"{Path_General}{nameof(Texture_SwordSlashWhite)}");
            Texture_SwordSlash1 = new Tex2DWithPath($"{Path_General}{nameof(Texture_SwordSlash1)}");
            Texture_SwordSlash2 = new Tex2DWithPath($"{Path_General}{nameof(Texture_SwordSlash2)}");
            Texture_Fog = new Tex2DWithPath($"{Path_General}{nameof(Texture_Fog)}");
            Texture_BloodStain = new Tex2DWithPath($"{Path_General}{nameof(Texture_BloodStain)}");
            Texture_RuShiWoWenFlower = new Tex2DWithPath($"{Path_General}{nameof(Texture_RuShiWoWenFlower)}");
            Texture_FireBall = new Tex2DWithPath($"{Path_General}{nameof(Texture_FireBall)}");
            Texture_FireBallPixel = new Tex2DWithPath($"{Path_General}{nameof(Texture_FireBallPixel)}");

        }
        public void UnloadTexture()
        {
            InvisAsset = null;
            ScarletGhost = null;
            Metaball_Bloody = null;

            Texture_BloomRing = null;
            Texture_BloomShockwave = null;
            Texture_SoftCircleEdge = null;
            Texture_WhiteCube = null;
            Texture_WhiteCubeBig = null;
            Texture_WhiteCircle = null;
            Texture_Spirite = null;
            Texture_EnergySword = null;
            Texture_RarityGlow = null;
            Texture_StandardGradient = null;
            Texture_SnowCloud = null;
            Texture_SwordSlash = null;
            Texture_SwordSlashWhite = null;
            Texture_SwordSlash1 = null;
            Texture_SwordSlash2 = null;
            Texture_Fog = null;
            Texture_BloodStain = null;
            Texture_RuShiWoWenFlower = null;
            Texture_FireBall = null;
            Texture_FireBallPixel = null;
        }
        public void LoadTrail()
        {
            Trail_ManaStreak = new Tex2DWithPath($"{Path_General}{nameof(Trail_ManaStreak)}");
            Trail_ManaStreakTiny = new Tex2DWithPath($"{Path_General}{nameof(Trail_ManaStreakTiny)}");
            Trail_ParaLine = new Tex2DWithPath($"{Path_General}{nameof(Trail_ParaLine)}");
            Trail_VShapeWithTail = new Tex2DWithPath($"{Path_General}{nameof(Trail_VShapeWithTail)}");
            Trail_RvSlash = new Tex2DWithPath($"{Path_General}{nameof(Trail_RvSlash)}");
            Trail_TerraRayFlow = new Tex2DWithPath($"{Path_General}{nameof(Trail_TerraRayFlow)}");
            Trail_ManaMegaBeam = new Tex2DWithPath($"{Path_General}{nameof(Trail_ManaMegaBeam)}");
            Trail_MegaBeam = new Tex2DWithPath($"{Path_General}{nameof(Trail_MegaBeam)}");
            Trail_FadedStreak = new Tex2DWithPath($"{Path_General}{nameof(Trail_FadedStreak)}");
            Trail_Lightning0 = new Tex2DWithPath($"{Path_General}{nameof(Trail_Lightning0)}");
            Trail_Lightning1 = new Tex2DWithPath($"{Path_General}{nameof(Trail_Lightning1)}");
            Trail_Lightning2 = new Tex2DWithPath($"{Path_General}{nameof(Trail_Lightning2)}");
            Trail_Lightning3 = new Tex2DWithPath($"{Path_General}{nameof(Trail_Lightning3)}");
            Trail_Lightning4 = new Tex2DWithPath($"{Path_General}{nameof(Trail_Lightning4)}");
            Trail_BloomDualLine = new Tex2DWithPath($"{Path_General}{nameof(Trail_BloomDualLine)}");
        }
        public static void UnloadTrail()
        {
            Trail_ManaStreak = null;
            Trail_ParaLine = null;
            Trail_RvSlash = null;
            Trail_VShapeWithTail = null;
            Trail_TerraRayFlow = null;
            Trail_ManaStreakTiny = null;
            Trail_FadedStreak = null;
            Trail_MegaBeam = null;
            Trail_ManaMegaBeam = null;
            Trail_Lightning0 = null;
            Trail_Lightning1 = null;
            Trail_Lightning2 = null;
            Trail_Lightning3 = null;
            Trail_Lightning4 = null;
            Trail_BloomDualLine = null;
        }
        public void LoadMisc()
        {
            Metaball_Bloody = new Tex2DWithPath($"{Path_Metaball}{nameof(Metaball_Bloody)}");

            Noise_Misc = new Tex2DWithPath($"{Path_General}{nameof(Noise_Misc)}");
            Noise_Misc2 = new Tex2DWithPath($"{Path_General}{nameof(Noise_Misc2)}");
            Noise_Aura = new Tex2DWithPath($"{Path_General}{nameof(Noise_Aura)}");
            Noise_EmptyAura = new Tex2DWithPath($"{Path_General}{nameof(Noise_EmptyAura)}");
            Noise_HeavyAura = new Tex2DWithPath($"{Path_General}{nameof(Noise_HeavyAura)}");
            Noise_Smoke = new Tex2DWithPath($"{Path_General}{nameof(Noise_Smoke)}");
            Noise_WaterFlow = new Tex2DWithPath($"{Path_General}{nameof(Noise_WaterFlow)}");
            Noise_BlackGalaxy1 = new Tex2DWithPath($"{Path_General}{nameof(Noise_BlackGalaxy1)}");
            Noise_BlackGalaxy2 = new Tex2DWithPath($"{Path_General}{nameof(Noise_BlackGalaxy2)}");

        }
        public static void UnloadMisc()
        {
            Metaball_Bloody = null;

            Noise_Misc = null;
            Noise_Misc2 = null;
            Noise_Aura = null;
            Noise_EmptyAura = null;
            Noise_Smoke = null;
            Noise_HeavyAura = null;
            Noise_WaterFlow = null;
            Noise_BlackGalaxy2 = null;
            Noise_BlackGalaxy1 = null;
        }
    }
}
