using Terraria;
using Terraria.ModLoader;
using YogsothothsYardMod.Content.Items.Weapon;
using YogsothothsYardMod.Content.Projs.Melee;
using YogsothothsYardMod.Globals.Graphics.Metaballs;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Globals.Players
{
    public partial class YardPlayer : ModPlayer
    {
        public int crimsonScytheHitCounter = 0;
        public int crimsonScytheAttackCounter = 0;
        public int crimsonScytheDefense = 0;
        public int crimsonScytheSlayNPCType = 0;
        public int globalSoundDelay = 0;
        public bool Executor_DrawFadeIn = false;
        public bool Executor_DrawFadeOut = false;
        public int NoSlowFall = 0;

        public override void PreUpdate()

        {
            base.PreUpdate();
        }
        public override void ResetEffects()
        {
            base.ResetEffects();
        }
        public override void UpdateDead()
        {
            crimsonScytheAttackCounter = 0;
        }
        public override void PostUpdate()
        {
            if (Player.HeldItem.type == ItemType<CrimsonScythe>() && !Player.HasProj<CrimsonScytheSkillProj>() && Main.mouseRight && Main.mouseRightRelease && Main.hoverItemName == "")
            {
                Vector2 dir = (Main.MouseWorld - Player.Center).SafeNormalize(Vector2.UnitX);
                foreach (var id in Main.ActiveProjectiles)
                {
                    if (id.type != ProjectileType<CrimsonScytheHeldProj>())
                        continue;
                    if (id.owner != Player.whoAmI)
                        continue;
                    id.ai[0] = 114514;
                    dir = id.velocity;
                    id.Kill();
                }
                Projectile proj = Projectile.NewProjectileDirect(Player.GetSource_FromThis(), Player.Center, dir, ProjectileType<CrimsonScytheSkillProj>(), 0, 0, Player.whoAmI);
                ((CrimsonScytheSkillProj)proj.ModProjectile).BeginTargetRotation = 0;
                ((CrimsonScytheSkillProj)proj.ModProjectile).Flip = true;
            }
            base.PostUpdate();
        }

        public override void PostUpdateEquips()
        {
            base.PostUpdateEquips();
        }
        public override void PostUpdateMiscEffects()
        {
            if (Player.HeldItem.type == ItemType<CrimsonScythe>() && crimsonScytheDefense > 0)
            {
                Player.statDefense += (int)crimsonScytheDefense;
                Vector2 pos = Player.ToRandRec();
                if (Player.miscCounter % 4 == 0)
                    BloodyMetaball.SpawnParticle(pos, -Vector2.UnitY, 0.4f, PiOver2);
            }
            if (globalSoundDelay > 0)
                globalSoundDelay--;
            if (crimsonScytheDefense > 0 && crimsonScytheAttackCounter < 1)
                crimsonScytheDefense -= 1;
        }
        public override void PostUpdateRunSpeeds()
        {
            if (NoSlowFall > 0)
            {
                Player.slowFall = false;
                Player.maxFallSpeed = 114514;
                Player.GoingDownWithGrapple = true;
            }
        }
    }
}
