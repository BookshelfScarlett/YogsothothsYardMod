using Terraria.ModLoader;

namespace YogsothothsYardMod.Globals.Players
{
    public partial class YardPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            tlipocaArmor = false;
            GlobalReset();
        }
        public override void UpdateDead()
        {
            crimsonScytheAttackCounter = 0;
            tlipocaArmor = false;
            GlobalReset();
        }
        private void GlobalReset()
        {
            critDamage = 1f;
            tlipocaArmor = false;
            infiniteFlightTime = false;
        }

    }
}
