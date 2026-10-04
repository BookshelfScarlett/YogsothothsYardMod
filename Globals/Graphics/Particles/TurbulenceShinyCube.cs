using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.ParticleSystem;

namespace YogsothothsYardMod.Globals.Graphics.Particles
{
    public class TurbulenceShinyCube : BaseParticle
    {
        public int RandPosMoveValue;
        public bool ShouldRandPosMove;
        public TurbulenceShinyCube(Vector2 position, Vector2 velocity, Color color, int lifetime, float Rot, float opacity, float scale, bool shouldRandPosMove = true, int randPosMoveValue = 13)
        {
            Position = position;
            Velocity = velocity;
            DrawColor = color;
            Lifetime = lifetime;
            Scale = scale;
            ShouldRandPosMove = shouldRandPosMove;
            RandPosMoveValue = randPosMoveValue;
        }
        public override void Update()
        {
            Opacity = (float)Math.Sin(LifetimeRatio * Pi);

            Velocity *= 0.95f;
            Scale *= 0.98f;

            if (Time % 3 == 0 && ShouldRandPosMove)
            {
                Position += Main.rand.NextVector2Circular(RandPosMoveValue, RandPosMoveValue);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = YardModAssets.Texture_WhiteCube.Value;
            spriteBatch.Draw(texture, Position - Main.screenPosition, null, DrawColor * Opacity, Rotation, texture.Size() / 2, Scale, SpriteEffects.None, 0f);
        }
    }
}
