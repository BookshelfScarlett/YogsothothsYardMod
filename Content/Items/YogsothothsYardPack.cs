using Terraria.ID;
using Terraria.ModLoader;
using YogsothothsYardMod.Content.Items.Armor.LittleLeaf;
using YogsothothsYardMod.Content.Items.Armor.Tlipoca;
using YogsothothsYardMod.Content.Items.Armor.XiaLuLing;
using YogsothothsYardMod.Content.Items.Armor.Yevna;
using YogsothothsYardMod.Content.Items.Weapon;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Content.Items
{
    public class YogsothothsYardPack : ModItem, ILocalizedModType
    {
        public override string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            ItemID.Sets.OpenableBag[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.rare = ItemRarityID.Red;

        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.AddCommon(ItemType<CrimsonScythe>());
            itemLoot.AddCommon(ItemType<TlipocaHead>());
            itemLoot.AddCommon(ItemType<TlipocaBody>());
            itemLoot.AddCommon(ItemType<TlipocaLegs>());
            itemLoot.AddCommon(ItemType<LittleLeafHead>());
            itemLoot.AddCommon(ItemType<LittleLeafBody>());
            itemLoot.AddCommon(ItemType<LittleLeafLegs>());
            itemLoot.AddCommon(ItemType<YevnaHead>());
            itemLoot.AddCommon(ItemType<YevnaBody>());
            itemLoot.AddCommon(ItemType<YevnaLegs>());
            itemLoot.AddCommon(ItemType<XiaLuLingHead>());
            itemLoot.AddCommon(ItemType<XiaLuLingBody>());
            itemLoot.AddCommon(ItemType<XiaLuLingLegs>());
        }
    }
}
