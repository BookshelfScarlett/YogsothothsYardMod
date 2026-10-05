using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using YogsothothsYardMod.Content.Items;
using YogsothothsYardMod.Content.Items.Weapon;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Globals.Players
{
    public partial class YardPlayer : ModPlayer
    {
        public int crimsonScytheHitCounter = 0;
        public int crimsonScytheAttackCounter = 0;
        public int crimsonScytheDefense = 0;
        public int crimsonScytheSlayNPCType = 0;
        public int maidReaperIndex = -1;
        public int maidReaperHealTimer = 0;
        public int globalSoundDelay = 0;
        public bool Executor_DrawFadeIn = false;
        public bool Executor_DrawFadeOut = false;
        public float critDamage = 1f;
        public bool tlipocaArmor = false;
        public int NoSlowFall = 0;
        public bool yardPack = false;
        public bool IsHoldingTlipocaScythe => Player.HeldItem.type == ItemType<CrimsonScythe>();
        public bool infiniteFlightTime = false;
        public int LegendaryLevel = YardMethods.GetLegendaryLevel();
        public override void LoadData(TagCompound tag)
        {
            yardPack = tag.GetBool("YardModGivePack");
            crimsonScytheSlayNPCType = tag.GetInt("YardModSlayingNPC");

        }
        public override void SaveData(TagCompound tag)
        {
                        tag.Add("YardModGivePack", yardPack);
            tag.Add("YardModSlayingNPC", crimsonScytheSlayNPCType);

        }
        public override void OnEnterWorld()
        {
            if (!yardPack)
            {
                Player.QuickSpawnItemDirect(Player.GetSource_FromThis(), ItemType<YogsothothsYardPack>());
                yardPack = true;
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (IsHoldingTlipocaScythe && YardMethods.GetLegendaryLevel()>= 15)
            {
                modifiers.DefenseEffectiveness *= 0;
            }
            if (tlipocaArmor && proj.DamageType.CountsAsClass<MeleeDamageClass>())
            {
                modifiers.CritDamage += critDamage;
                if (IsHoldingTlipocaScythe)
                {
                    if (target.IsLegal())
                        maidReaperIndex = target.whoAmI;
                }
            }
        }
    }
}
