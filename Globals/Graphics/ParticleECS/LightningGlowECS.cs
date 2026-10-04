using Microsoft.Xna.Framework.Graphics;
using Terraria;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.ParticleECS;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Globals.Graphics.ParticleECS
{
    public class LightningGlowECS : ECSParticleBehavior
    {
        public override void OnSpawn(ref ECSParticleData data)
        {

            base.OnSpawn(ref data);
        }
        public override void Update(ref ECSParticleData data)
        {
            data.DrawColor = Color.Lerp(data.DrawColor, Color.Transparent, .105f);
        }
        public override void Draw(ref ECSParticleData data)
        {
            int drawTime = (int)data.aifloat0;
            Vector2 pos = data.Position - Main.screenPosition;
            Texture2D tex = YardModAssets.Particle_SharpTear;
            Vector2 scale = new(.5f, 3.5f);
            for (int i = 0; i < drawTime; i++)
            {
                Vector2 offsetVec = data.Velocity.ToSafeNormalize() * i * 1.4f;
                Main.spriteBatch.Draw(tex, pos + offsetVec, null, data.DrawColor, data.Velocity.ToRotation() + PiOver2, tex.Size() / 2f, data.Scale * scale, 0, 0);
            }
        }
    }
}
