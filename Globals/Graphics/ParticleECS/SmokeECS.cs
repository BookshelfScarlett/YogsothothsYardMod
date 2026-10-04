using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.ParticleECS;

namespace YogsothothsYardMod.Globals.Graphics.ParticleECS
{
    public class SmokeParticleECS : ECSParticleBehavior
    {

        public override void Update(ref ECSParticleData particleDate)
        {
            particleDate.Velocity *= .9f;
            particleDate.Velocity.RotatedBy(Main.rand.NextFloat(ToRadians(-5f), ToRadians(5f)));
            particleDate.Opacity = Lerp(particleDate.Opacity, 0, 0.12f);
        }
        public override void Draw(ref ECSParticleData data)
        {
            Asset<Texture2D> texture = data.aifloat0 > 0 ? YardModAssets.Particle_SmokeAlt.Texture : YardModAssets.Particle_Smoke.Texture;

            Rectangle frame = texture.Frame(4, 4, (int)(data.LifetimeRatio * 16) % 4, (int)(data.LifetimeRatio * 4));
            Vector2 origin = frame.Size() * 0.5f;
            Main.spriteBatch.Draw(texture.Value, data.Position - Main.screenPosition, frame, data.DrawColor * data.Opacity, data.Rotation, origin, data.Scale, SpriteEffects.None, 0);
        }
    }
}
