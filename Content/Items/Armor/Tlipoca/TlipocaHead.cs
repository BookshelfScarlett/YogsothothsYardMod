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
        public float Damage = .05f;
        public float Crit = .05f;
        public float CritDamage = .05f;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Damage.ToPercent());
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
            player.GetDamage<MeleeDamageClass>() += Damage;
        }
        public override void UpdateArmorSet(Player player)
        {
            int level = YardMethods.GetLegendaryLevel();
            player.lifeRegen += 4;
            float damageMult = .02f + .03f * (level);
            float crit = 2 + 1 * (level) + 2; //8
            float critDamage = .06f + .04f * (level / 2) - .08f;
            int defense = 4 + 6 * level;
            player.YardMod().tlipocaArmor = true;
            player.statDefense += defense;
            player.GetDamage<MeleeDamageClass>() += damageMult;
            player.GetCritChance<MeleeDamageClass>() += crit;
            player.setBonus += this.GetLocalizationKey("SetBonus").ToLangValue().ToFormatValue(damageMult.ToPercent(), crit + "%", critDamage.ToPercent(), defense);
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
