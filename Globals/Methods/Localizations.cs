using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using YogsothothsYardMod.Content.Rarity.Helper;
namespace YogsothothsYardMod.Globals.Methods
{
    public static partial class YardMethods
    {
        public static void ReplaceAllTooltip(this List<TooltipLine> tooltips, string replacedTextPath, Color? textColor = null)
        {
            tooltips.RemoveAll((line) => line.Mod == "Terraria" && line.Name != "Tooltip0" && line.Name.StartsWith("Tooltip"));
            TooltipLine getTooltip = tooltips.FirstOrDefault((x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
            string formateText = replacedTextPath.ToLangValue();
            Color overrideColor = textColor ?? Color.White;
            if (getTooltip is not null)
            {
                getTooltip.Text = formateText;
                getTooltip.OverrideColor = overrideColor;
            }
        }
        /// <summary>
        /// 干翻所有Tooltip，并借助本地化完全重写一次，重载染色，附带键入值
        /// </summary>
        /// <param name="tooltips"></param>
        /// <param name="replacedTextPath"></param>
        /// <param name="args"></param>
        public static void ReplaceAllTooltip(this List<TooltipLine> tooltips, string replacedTextPath, Color? textColor = null, params object[] args)
        {
            tooltips.RemoveAll((line) => line.Mod == "Terraria" && line.Name != "Tooltip0" && line.Name.StartsWith("Tooltip"));
            TooltipLine getTooltip = tooltips.FirstOrDefault((x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
            string formateText = replacedTextPath.ToLangValue().ToFormatValue(args);
            Color overrideColor = textColor ?? Color.White;
            if (getTooltip is not null)
            {
                getTooltip.Text = formateText;
                getTooltip.OverrideColor = textColor;
            }
        }
        public static void CreateTooltip(this List<TooltipLine> tooltips, string textPath, Color? color = null, string LineName = "YogsothothMod", int index = -1)
        {
            string text = textPath.ToLangValue();
            Mod tooltipMod = YogsothothsYardMod.Instance;
            Color overrideColor = color ?? Color.White;
            var newLine = new TooltipLine(tooltipMod, LineName + "Name", text)
            {
                OverrideColor = overrideColor
            };
            int count = tooltips.Count;
            if (count is 0)
                tooltips.Add(newLine);
            else
            {
                if (index != -1)
                    count = index;
                tooltips.Insert(count, newLine);
            }
        }
        public static void CreateTooltip(this List<TooltipLine> tooltips, string textPath, Color? color = null, string LineName = "YogsothothMod", int index = -1, params object[] args)
        {
            string text = textPath.ToLangValue().ToFormatValue(args);
            Mod tooltipMod = YogsothothsYardMod.Instance;
            Color overrideColor = color ?? Color.White;
            var newLine = new TooltipLine(tooltipMod, LineName + "Name", text)
            {
                OverrideColor = overrideColor
            };
            int count = tooltips.Count;
            if (count is 0)
                tooltips.Add(newLine);
            else
            {
                if (index != -1)
                    count = index;
                tooltips.Insert(count, newLine);
            }
        }
        public static void CreateTooltipDirect(this List<TooltipLine> tooltips, string textPath, Color? color = null, string LineName = "YogsothothMod", int index = -1)
        {
            string text = textPath;
            Mod tooltipMod = YogsothothsYardMod.Instance;
            Color overrideColor = color ?? Color.White;
            var newLine = new TooltipLine(tooltipMod, LineName + "Name", text)
            {
                OverrideColor = overrideColor
            };
            int count = tooltips.Count;
            if (count is 0)
                tooltips.Add(newLine);
            else
            {
                if (index != -1)
                    count = index;
                tooltips.Insert(count, newLine);
            }
        }

        public static void CreateTooltipDirect(this List<TooltipLine> tooltips, string textValue, Color? color = null, string LineName = "YogsothothMod", int index = -1, params object[] args)
        {
            string text = textValue.ToFormatValue(args);
            Mod tooltipMod = YogsothothsYardMod.Instance;
            Color overrideColor = color ?? Color.White;
            var newLine = new TooltipLine(tooltipMod, LineName + "Name", text)
            {
                OverrideColor = overrideColor
            };
            int count = tooltips.Count;
            if (count is 0)
                tooltips.Add(newLine);
            else
            {
                if (index != -1)
                    count = index;
                tooltips.Insert(count, newLine);
            }
        }
        public static int FindLineIndex(this List<TooltipLine> tooltips, string lineName, string lineMod = "Terraria") => tooltips.FindIndex(t => t.Name == lineName && t.Mod == lineMod);
        public static string ToLangValue(this string textPath) => Language.GetTextValue(textPath);

        public static string ToFormatValue(this string baseTextValue, params object[] args)
        {
            try
            {
                return string.Format(baseTextValue, args);
            }
            catch
            {
                return baseTextValue + "格式化出错";
            }
        }
        public static void ApplyFlavorTooltipLine(this List<TooltipLine> tooltips)
        {

        }
        public static void ApplyLegendaryTooltipline(this List<TooltipLine> line)
        {
            int index = -1;
            int firstLine = line.FindIndex(t => t.Name.Contains("Tooltip") && t.Mod == "Terraria");
            index = firstLine;
            for (int i = firstLine; i < line.Count; i++)
            {
                if (line[i].Name.Contains("Tooltip") && line[i].Mod == "Terraria")
                    index++;
                else
                    break;
            }
            string ownerPrefix = ("Mods.YogsothothsYardMod.Database.GenericText.LegendaryItem").ToLangValue();

            string tooltip = $"{ownerPrefix}";
            line.CreateTooltipDirect(tooltip, Color.White, "LegendaryItem", index);
            string upgradeTooltip = ("Mods.YogsothothsYardMod.Database.GenericText.UpgradeProgress").ToLangValue().ToFormatValue(GetLegendaryLevel(), 16);
            line.CreateTooltipDirect(upgradeTooltip, Color.White, "UpgradeProgress", index + 1);

            //string tooltip = $"「{ownerPrefix}」";
            //line.CreateTooltipDirect(tooltip, Color.White, "LegendaryItem", index);
            //string upgradeTooltip = ("Mods.YogsothothsYardMod.Database.GenericText.UpgradeProgress").ToLangValue().ToFormatValue(GetLegendaryLevel(), 16);
            //line.CreateTooltipDirect("「"+upgradeTooltip+"」", Color.White, "UpgradeProgress", index + 1);

        }
        public static void ApplyColorForLegendaryData(this DrawableTooltipLine line, Color edgeColor, Color mainColor)
        {
            if (line.Name == "LegendaryItemName" && line.Mod == "YogsothothsYardMod")
            {
                RarityDrawHelper.DrawCustomTooltipLine(line, edgeColor, mainColor);
            }
            if (line.Name == "UpgradeProgressName" && line.Mod == "YogsothothsYardMod")
            {
                RarityDrawHelper.DrawCustomTooltipLine(line, edgeColor, mainColor);
            }
        }

        public static int GetLegendaryLevel()
        {
            int level = 0;
            int add(Condition cum)
            {
                return cum.IsMet().ToInt();
            }
            int totalAdd =
                add(Condition.DownedKingSlime) +
                add(Condition.DownedEyeOfCthulhu) +
                add(Condition.DownedEowOrBoc) +
                add(Condition.DownedQueenBee) +
                add(Condition.DownedSkeletron) +
                Main.hardMode.ToInt() +
                add(Condition.DownedQueenSlime) +
                add(Condition.DownedTwins) +
                add(Condition.DownedDestroyer) +
                add(Condition.DownedDestroyer) +
                add(Condition.DownedPlantera) +
                add(Condition.DownedGolem) +
                add(Condition.DownedEmpressOfLight) +
                add(Condition.DownedDukeFishron) +
                add(Condition.DownedCultist) +
                add(Condition.DownedMoonLord);
            return level + totalAdd;
        }
    }
}
