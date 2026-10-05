using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YogsothothsYardMod.Globals.Methods
{
    public static partial class YardMethods
    {
        public static void SetUpNoUseGraphicItem(this Item item, bool channel = false, bool autoReuse = true)
        {
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = channel;
            item.autoReuse = autoReuse;
        }
        public static int ApplyWeaponAttackSpeed(this Player player, Item item, int time, int Min)
        {
            float a = player.GetWeaponAttackSpeed(item);
            float Mult = 1f / a;
            int RealAttack = (int)(time * Mult);
            if (RealAttack < Min)
                return Min;
            else
                return RealAttack;
        }
        public static bool IsLegal(this Item item)
        {
            return !item.IsAir && item is not null;
        }
        public static bool IsTool(this Item item)
        {
            return item.IsLegal() && (item.pick > 0 || item.axe > 0 || item.hammer > 0);
        }
        public static bool IsWeapon(this Item item)
        {
            return !item.IsTool() && (item.damage > 0 || item.type == ItemID.CoinGun);
        }
        public static bool IsHolding<T>(this Player player) where T : ModItem
            => player.HeldItem.type == ItemType<T>();
    }
}
