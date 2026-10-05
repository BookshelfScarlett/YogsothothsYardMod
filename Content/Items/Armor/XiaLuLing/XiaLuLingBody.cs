using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using YogsothothsYardMod.Content.Rarity.Types;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Content.Items.Armor.XiaLuLing
{
    [AutoloadEquip(EquipType.Body)]
    public class XiaLuLingBody : ModItem, ILocalizedModType
    {
        //public override string Texture => $"{GetType().Namespace}.XiaLuLingHead".Replace('.', '/');
        public override string LocalizationCategory => "Items.Armor";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.vanity = true;
            Item.YardMod().drawGhostIcon = true;
            Item.rare = RarityType<XiaLuLingRarity>();
        }
        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
        }
        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);
        }
        public override void PostDrawTooltipLine(DrawableTooltipLine line)
        {
            if (line.Name == "ItemName" && line.Mod == "Terraria")
            {
                XiaLuLingRarity.DrawItemName(line);
            }
            //line.ApplyColorForLegendaryData(XiaLuLingRarity.EdgeColor, XiaLuLingRarity.MainColor);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
        }
    }
}
