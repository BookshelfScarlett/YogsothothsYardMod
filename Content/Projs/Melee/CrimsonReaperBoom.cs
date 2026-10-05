using Terraria;
using Terraria.ModLoader;
using YogsothothsYardMod.Assets.Register;

namespace YogsothothsYardMod.Content.Projs.Melee
{
    public class CrimsonReaperBoom : ModProjectile, ILocalizedModType
    {
        public override string LocalizationCategory => "Projs.Melee";
        public override string Texture => YardModAssets.InvisAsset.Path;
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 60;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.timeLeft = 60;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.penetrate = 1;
            Projectile.localNPCHitCooldown = 60;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return base.Colliding(projHitbox, targetHitbox);
        }
        public override bool? CanHitNPC(NPC target)
        {
            return null;
        }
        public override void AI()
        {
            base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}
