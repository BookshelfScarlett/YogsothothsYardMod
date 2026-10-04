using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using YogsothothsYardMod.Content.Rarity.Helper;
using YogsothothsYardMod.Content.Rarity.Sparkles;

namespace YogsothothsYardMod.Content.Rarity.Types
{
    public class XiaLuLingRarity : ModRarity
    {
        public override Color RarityColor => Color.LimeGreen;
        public static List<RaritySparkle> RaritySparkles = [];
        public static List<RaritySparkle> FlavorSparkles = [];
        public static Color GlowColor = Color.Lerp(Color.White, Color.Green, 0.35f);
        public static Color EdgeColor = Color.Lerp(Color.White, Color.DarkGreen, 0.95f);
        public static Color MainColor = Color.White;
        public static void DrawItemName(DrawableTooltipLine line)
        {
            PostDrawRarity(ref RaritySparkles, line, Color.Green, Color.White);
            RarityDrawHelper.DrawCustomTooltipLine(line, GlowColor, EdgeColor, MainColor, 1);
        }
        public static void DrawFlavorTooltipName(DrawableTooltipLine line)
        {
            PostDrawRarity(ref FlavorSparkles, line, Color.Green, Color.White);
            RarityDrawHelper.DrawCustomTooltipLine(line, GlowColor, EdgeColor, MainColor, 1);
        }
        public static void PostDrawRarity(ref List<RaritySparkle> particleList, DrawableTooltipLine tooltipLine, Color c, Color c2, bool slowdown = false)
        {
            //在这里手动创建新的粒子，然后我们再将其添加进需要的表单内
            Vector2 textSize = tooltipLine.Font.MeasureString(tooltipLine.Text);
            if (Main.rand.NextBool(10))
            {
                float scale = Main.rand.NextFloat(0.30f * 0.5f, 0.30f) * 1.2f;
                int lifetime = 160;
                Vector2 position = GetParticlePosition(tooltipLine);
                Vector2 velocity = -Vector2.UnitY * Main.rand.NextFloat(0.25f, 0.55f) * (1 + slowdown.ToInt() * -0.75f);
                RarityShinyOrb rarityShinyOrb = new(position, velocity, RandLerpColor(c, c2), lifetime, scale);
                particleList.Add(rarityShinyOrb);
            }
            //最后更新他。
            RarityDrawHelper.UpdateTooltipParticles(tooltipLine, ref particleList);
        }
        public static Vector2 GetParticlePosition(DrawableTooltipLine line)
        {
            Vector2 textSize = line.Font.MeasureString(line.Text);
            return Main.rand.NextVector2FromRectangle(new(-(int)(textSize.X * 0.5f), -(int)(textSize.Y * 0.5f), (int)textSize.X, (int)(textSize.Y * 0.35f)));
        }
    }
}
