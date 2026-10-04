using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.Database.Enums;
using YogsothothsYardMod.Core.ParticleECS;
using YogsothothsYardMod.Core.PixelatedRender;
using YogsothothsYardMod.Core.Primitives.Trail;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Content.Projs.Melee
{
    public class CrimsonScytheHealingSoul : ModProjectile, ILocalizedModType, IPixelatedRenderer
    {
        public Player Owner => Main.player[Projectile.owner];
        public override string LocalizationCategory => "Projs.Melee";
        public override string Texture => YardModAssets.InvisAsset.Path;
        public ref float Timer => ref Projectile.ai[0];
        public enum State
        {
            Shoot,
            Homing,
            HomingTarget,
            Fade
        }
        public State AttackState
        {
            get => (State)Projectile.ai[1];
            set => Projectile.ai[1] = (float)value;
        }
        public int HealAmount
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        public int CurSelected
        {
            get => (int)Projectile.localAI[0];
            set => Projectile.localAI[0] = (float)value;
        }
        public override void SetStaticDefaults()
        {
            Projectile.ToTrailSetting(24);
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.noEnchantmentVisuals = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.extraUpdates = 2;
        }
        public static List<Color> BeginColor = new List<Color>()
        {
            Color.HotPink,Color.DeepSkyBlue,Color.LightGray,Color.DarkRed,Color.Purple,Color.DarkGreen
        };
        public static List<Color> EndColor = new List<Color>()
        {
            Color.LightPink,Color.LightSkyBlue,Color.White,Color.Crimson,Color.Violet,Color.LimeGreen
        };
        public void OnFirstFrame()
        {

            Color c1 = BeginColor[CurSelected];
            Color c2 = EndColor[CurSelected];

            for (int i = 0; i < 16; i++)
            {
                ECSParticle.ShinyCrossStarECS(Projectile.Center.ToRandCirclePos(16), Projectile.velocity.ToRandVelocity(ToRadians(30), 1.2f, 7.4f), RandLerpColor(c1, c2), 45, 1, Main.rand.NextFloat(.85f, 1.15f) * .34f, 0.2f);
                ECSParticle.LightntingGlow(Projectile.Center.ToRandCirclePosEdge(8), Projectile.velocity.ToRandVelocity(0, 1.2f, 7.4f), RandLerpColor(c1, c2), 45, 1, Main.rand.NextFloat(.85f, 1.15f) * .4f);
            }
        }
        public override void AI()
        {
            if (!Projectile.YardMod().FirstFrame)
                OnFirstFrame();
            ProjAI();
        }
        public void ProjAI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Timer++;
            if (AttackState == State.Shoot)
            {
                Projectile.velocity *= 0.97f;
                Timer++;
                TrailDust();
                if (Timer > Projectile.MaxUpdates * 30f)
                {
                    Projectile.netUpdate = true;
                    AttackState = State.Homing;
                    Timer = 0;
                }
            }
            else if (AttackState == State.Homing)
            {
                float maxTime = 30 * Projectile.MaxUpdates;
                float progress = Utils.GetLerpValue(0, maxTime, Timer, true);
                float lerpSpeed = Lerp(0.1f, 17f, progress);
                float lerpAngle = Lerp(0f, 15f, EaseInCubic(progress));
                Projectile.HomingTarget(Owner.Center, -1, lerpSpeed, 20, lerpAngle);
                //完全确定可以执行转弯了才会播报这些粒子
                //避免棱形粒子在转弯时出戏
                if (progress == 1f)
                    TrailDust();
                if (Projectile.Hitbox.Intersects(Owner.Hitbox))
                {
                    AttackState = State.Fade;
                    Timer = 0;
                    Owner.ScarletHeal(2, Color.LimeGreen);
                }
            }
            else if (AttackState == State.HomingTarget)
            {
                NPC curTar = Main.npc[Projectile.YardMod().GlobalTargetIndex];
                if (curTar.IsLegal())
                {
                    float maxTime = 30 * Projectile.MaxUpdates;
                    float progress = Utils.GetLerpValue(0, maxTime, Timer, true);
                    float lerpSpeed = Lerp(0.1f, 17f, progress);
                    float lerpAngle = Lerp(0f, 15f, EaseInCubic(progress));
                    Projectile.HomingTarget(curTar.Center, -1, lerpSpeed, 20, lerpAngle);
                    //完全确定可以执行转弯了才会播报这些粒子
                    //避免棱形粒子在转弯时出戏
                    if (progress == 1f)
                        TrailDust();
                }
            }
            else if (AttackState == State.Fade)
            {

                Projectile.Opacity *= 0.95f;
                Projectile.velocity *= 0.02f;
                if (Projectile.Opacity < 0.08f)
                {
                    Projectile.Kill();
                }
            }
            else
            {

            }
        }
        public void TrailDust()
        {
            if (Projectile.IsOutScreen())
                return;
            Color c1 = BeginColor[CurSelected];
            Color c2 = EndColor[CurSelected];
            if (Main.rand.NextBool(4))
                ECSParticle.ShinyCrossStarECS(Projectile.Center.ToRandCirclePos(16), Projectile.velocity / 8f, RandLerpColor(c1, c2), 45, 1, Main.rand.NextFloat(.85f, 1.15f) * .34f, 0.2f);
            if (Main.rand.NextBool(8))
                ECSParticle.LightntingGlow(Projectile.Center.ToRandCirclePosEdge(8), Projectile.velocity / 8f, RandLerpColor(c1, c2), 45, 1, Main.rand.NextFloat(.85f, 1.15f) * .4f);
        }
        public override bool? CanDamage() => AttackState == State.HomingTarget;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (AttackState == State.HomingTarget)
            {
                AttackState = State.Fade;
                Timer = 0;
                Projectile.netUpdate = true;
            }
        }
        public SpriteBatch SB { get => Main.spriteBatch; }
        public GraphicsDevice GD { get => Main.graphics.GraphicsDevice; }
        public BlendState BlendState => BlendState.Additive;
        public ScarletDrawLayer LayerToRenderTo => ScarletDrawLayer.BeforeDusts;
        public void RenderPixelated(SpriteBatch spriteBatch)
        {
            if (!Projectile.YardMod().FirstFrame)
                return;
            YardMethods.EnterShaderAreaPixel(BlendState.Additive);
            Texture2D orb = YardModAssets.Texture_Spirite.Value;
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            float scale = Projectile.scale * 0.40f * Lerp(1f, 1.12f, (float)(Math.Abs(Math.Sin(Main.GlobalTimeWrappedHourly)))) * Projectile.Opacity;
            Vector2 newVec = new Vector2(1) * .93f;
            float rot = Main.GlobalTimeWrappedHourly * 1.2f + Projectile.rotation;
            Color firstC = BeginColor[CurSelected];
            Color endC = EndColor[CurSelected];

            SB.Draw(orb, drawPos, null, firstC * 1f, rot, orb.Size() / 2f, scale * newVec, 0, 0);
            orb = YardModAssets.Particle_HRShinyOrb.Value;
            SB.Draw(orb, drawPos, null, Color.White * 1f, rot, orb.Size() / 2f, scale * newVec * 0.85f, 0, 0);

            TrailFunc(YardModAssets.Trail_ManaStreak.Texture, 1f, firstC * 0.65f);
            TrailFunc(YardModAssets.Trail_Lightning0.Texture, 0.8f, endC * 0.95f);
            TrailFunc(YardModAssets.Trail_Lightning0.Texture, .5f, Color.White * 0.85f);

            YardMethods.EndShaderAreaPixel();
        }
        public void TrailFunc(Asset<Texture2D> trail, float mult, Color c)
        {
            Effect shader = YardModShader.StandardFlowShader;
            float laserLength = 50;
            shader.Parameters["LaserTextureSize"].SetValue(trail.Size());
            shader.Parameters["targetSize"].SetValue(new Vector2(laserLength, trail.Height()));
            shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly * -18.2f);
            shader.Parameters["uColor"].SetValue(c.ToVector4() * Projectile.Opacity * Clamp(Projectile.velocity.Length(), 0f, 1f));
            shader.Parameters["uFadeoutLength"].SetValue(0.8f);
            shader.Parameters["uFadeinLength"].SetValue(0.15f);
            shader.CurrentTechnique.Passes[0].Apply();
            DrawSetting sets = new(trail.Value);
            List<TrailDrawDate> date = [];
            int length = (int)(Projectile.oldPos.Length * Projectile.Opacity * Clamp(Projectile.velocity.Length(), 0, 1));
            for (int i = 0; i < length; i++)
            {
                if (Projectile.oldPos[i] == Vector2.Zero)
                    continue;
                Vector2 listPos = Projectile.oldPos[i] + Projectile.Size / 2 + Projectile.SafeDir() * 10f;
                float ratios = i / (float)length;
                date.Add(new(listPos, Color.White, new(0, 40 * mult * Clamp((1 - ratios), 0.32f, 1f)), Projectile.oldRot[i]));
            }
            TrailRender.DrawTrail(date.ToArray(), sets);

        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (!Projectile.YardMod().FirstFrame)
                return false;
            PixelatedRenderManager.BeginDrawProj = true;
            return false;
        }
    }
}
