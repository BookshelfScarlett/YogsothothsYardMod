using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YogsothothsYardMod.Content.Rarity.Types;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Content.Items.Armor.Tlipoca
{
    [AutoloadEquip(EquipType.Body)]
    public class TlipocaBody : ModItem, ILocalizedModType
    {
        public override string LocalizationCategory => "Items.Armor";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override LocalizedText Tooltip => base.Tooltip;
        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.defense = 4;
            Item.YardMod().drawGhostIcon = true;
            Item.rare = ItemRarityID.Red;
            Item.rare = RarityType<TlipocaRarity>();
        }
        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
        }
        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.ApplyLegendaryTooltipline();
        }
        public override void PostDrawTooltipLine(DrawableTooltipLine line)
        {
            if (line.Name == "ItemName" && line.Mod == "Terraria")
            {
                TlipocaRarity.DrawItemName(line);
            }
            line.ApplyColorForLegendaryData(Color.Red, Color.Black);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
        }
    }
}
