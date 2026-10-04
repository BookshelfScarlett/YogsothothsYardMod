using Terraria;
using Terraria.ModLoader;
using YogsothothsYardMod.Content.Rarity.Types;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Content.Items.Armor.LittleLeaf
{
    [AutoloadEquip(EquipType.Body)]
    public class LittleLeafBody : ModItem, ILocalizedModType
    {
        //public override string Texture => $"{GetType().Namespace}.LittleLeafHead".Replace('.', '/');
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
            Item.rare = RarityType<LittleLeaftRarity>();
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
                LittleLeaftRarity.DrawItemName(line);
            }
            line.ApplyColorForLegendaryData(LittleLeaftRarity.EdgeColor, LittleLeaftRarity.MainColor);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
        }
    }
}
