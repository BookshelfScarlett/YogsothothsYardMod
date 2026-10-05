using Terraria;
using Terraria.ModLoader;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Content.Items.Weapon;
using YogsothothsYardMod.Content.Projs.Melee;
using YogsothothsYardMod.Core.Systems;
using YogsothothsYardMod.Globals.Graphics.Metaballs;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Globals.Players.Dashes;

namespace YogsothothsYardMod.Globals.Players
{
    public partial class YardPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            if (Player.HeldItem.type == ItemType<CrimsonScythe>() && !Player.HasProj<CrimsonScytheSkillProj>() 
                && Main.mouseRight && Main.mouseRightRelease && Player.HasProj<CrimsonScytheSoulStone>() &&!Main.mapFullscreen&& Main.hoverItemName == "")
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
            TlipocaArmorCompanion();
        }

        public void TlipocaArmorCompanion()
        {
            if (!(Player.IsHolding<CrimsonScythe>()))
                return;
            Player.noKnockback = true;
            Player.noFallDmg = true;
            if (YardMethods.GetLegendaryLevel()>= 11)
            {
                infiniteFlightTime = true;
                Player.ApplyDash(GetInstance<CrimsonScytheDash>().Type);
            }

            if (tlipocaArmor)
            {
                Player.jumpSpeed += 1.6f;
                Player.runAcceleration *= 1.25f;
                Player.moveSpeed += .35f;
                Player.GetDamage<MeleeDamageClass>() += .10f;
                Player.GetCritChance<MeleeDamageClass>() += 10;
                Player.GetAttackSpeed<MeleeDamageClass>() += .10f;
                Player.statDefense += (int)(Player.statDefense * .15f);
                if (YardModKeybings.GeneralSkillKeybind.JustPressed && maidReaperIndex != -1 && IsHoldingTlipocaScythe && maidReaperHealTimer == 0)
                {
                    NPC npc = Main.npc[maidReaperIndex];
                    if (npc.IsLegal())
                    {
                        ScarletSound(YardModSounds.Tlipoca_SoulAbsorb, Player.Center, 0.85f, 1, 0.3f);
                        float ratios = Clamp((float)crimsonScytheHitCounter / (float)40, 0, 1);
                        Projectile proj = Projectile.NewProjectileDirect(Player.GetSource_FromThis(), npc.Center, RandVelTwoPi(6, 9), ProjectileType<CrimsonReaperHeal>(), 0, 0, Player.whoAmI);
                        proj.ai[2] = ratios;
                        ((CrimsonReaperHeal)proj.ModProjectile).CurTarget = npc;
                        crimsonScytheHitCounter = 0;
                        maidReaperHealTimer = GetSeconds((int)(Lerp(0, 10, ratios)));
                    }
                }
            }
        }
        public override void PostUpdateMiscEffects()
        {
            if (Player.IsHolding<CrimsonScythe>() && crimsonScytheDefense > 0)
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
            if (maidReaperHealTimer > 0)
                maidReaperHealTimer--;
            if (NoSlowFall > 0)
                NoSlowFall--;
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
