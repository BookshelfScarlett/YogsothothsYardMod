using Microsoft.Xna.Framework.Graphics;
using Terraria;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.ParticleECS;

namespace YogsothothsYardMod.Globals.Graphics.ParticleECS
{
    public class HRShinyOrbECS : ECSParticleBehavior
    {
        public override void OnSpawn(ref ECSParticleData data)
        {

        }
        public override void Update(ref ECSParticleData data)
        {
            data.Velocity *= .92f;
            data.Scale = Lerp(data.Scale, 0, EaseInCubic(data.LifetimeRatio));
        }
        public override void Draw(ref ECSParticleData data)
        {
            //中心辉光大小
            float glowMultValue = data.aifloat0;
            Texture2D orb = YardModAssets.Particle_HRShinyOrb.Value;
            Main.spriteBatch.Draw(orb, data.Position - Main.screenPosition, null, data.DrawColor * data.Opacity, 0, orb.Size() / 2f, data.Scale, 0, 0);
            if (glowMultValue > 0)
                Main.spriteBatch.Draw(orb, data.Position - Main.screenPosition, null, Microsoft.Xna.Framework.Color.White * data.Opacity, 0, orb.Size() / 2f, data.Scale * glowMultValue, 0, 0);
        }
    }
}
