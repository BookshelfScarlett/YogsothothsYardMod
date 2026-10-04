using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YogsothothsYardMod.Content.Rarity.Types;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Content.Items.Armor.Tlipoca
{
    [AutoloadEquip(EquipType.Head)]
    public class TlipocaHead : ModItem, ILocalizedModType
    {
        public override string LocalizationCategory => "Items.Armor";
        public override LocalizedText Tooltip => base.Tooltip;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.defense = 4;
            Item.YardMod().drawGhostIcon = true;
            Item.rare = ItemRarityID.Red;
            Item.rare = RarityType<TlipocaRarity>();
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == Type && body.type == ItemType<TlipocaBody>() && legs.type == ItemType<TlipocaLegs>();
        }
        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus += this.GetLocalizationKey("SetBonus").ToLangValue();
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
