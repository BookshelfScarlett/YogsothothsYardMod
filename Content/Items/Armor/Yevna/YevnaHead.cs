using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using YogsothothsYardMod.Content.Rarity.Types;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Content.Items.Armor.Yevna
{
    [AutoloadEquip(EquipType.Head)]
    public class YevnaHead : ModItem, ILocalizedModType
    {
        public override string LocalizationCategory => "Items.Armor";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.defense = 4;
            Item.YardMod().drawGhostIcon = true;
            Item.rare = RarityType<YevnaRarity>();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == Type && body.type == ItemType<YevnaBody>() && legs.type == ItemType<YevnaLegs>();
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
                YevnaRarity.DrawItemName(line);
            }
            line.ApplyColorForLegendaryData(YevnaRarity.EdgeColor, YevnaRarity.MainColor);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
        }
    }
}
