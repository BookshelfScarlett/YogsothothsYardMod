using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using YogsothothsYardMod.Content.Buffs;
using YogsothothsYardMod.Content.Projs.Melee;
using YogsothothsYardMod.Content.Rarity.Types;
using YogsothothsYardMod.Core.Database.Localizations;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Globals.Methods.Textbox;

namespace YogsothothsYardMod.Content.Items.Weapon
{
    public class CrimsonScythe : ModItem, ILocalizedModType
    {
        public override string LocalizationCategory => "Items.Weapons.Melee";
        public override bool MeleePrefix() => true;
        public static int ExecutionProgress => 40;
        public static int DefensePerAdd = 2;
        public static int MaxSoulStone = 20;
        public static int BaseDamage = 12;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.damage = BaseDamage;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = Item.useAnimation = 38;
            Item.SetUpNoUseGraphicItem(true);
            Item.rare = RarityType<TlipocaRarity>();
            Item.YardMod().drawGhostIcon = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileType<CrimsonScytheHeldProj>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
        }
        public override bool CanShoot(Player player)
        {
            return !player.HasProj(Item.shoot) && !player.HasProj<CrimsonScytheSkillProj>();
        }
        public override bool CanRightClick() => Main.keyState.PressingShift();
        public override void RightClick(Player player)
        {
            player.YardMod().crimsonScytheSlayNPCType += 1;
            if (player.YardMod().crimsonScytheSlayNPCType > 2)
                player.YardMod().crimsonScytheSlayNPCType = 0;
        }
        public override bool ConsumeItem(Player player) => false;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bool exe = player.YardMod().crimsonScytheHitCounter >= ExecutionProgress;
            Vector2 dir = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX);
            Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            proj.YardMod().HasExecutionMechanic = true;
            proj.YardMod().ExecutionStrike = exe;
            ((CrimsonScytheHeldProj)proj.ModProjectile).BeginTargetRotation = dir.ToRotation();
            ((CrimsonScytheHeldProj)proj.ModProjectile).Flip = Main.rand.NextBool();
            return false;
        }
        public IReadOnlyList<TooltipLine> CacheTooltipList = null;
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            bool isPressingLeftAlt = Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt);
            int requirements = Math.Max(0, ExecutionProgress);
            tooltips.ApplyLegendaryTooltipline();
            ExModifyTooltips(tooltips);
            int killerType = tooltips.FindLastIndex(t => t.Name.Contains("Tooltip") && t.Mod == "Terraria");
            int killerIndex = Main.LocalPlayer.YardMod().crimsonScytheSlayNPCType;
            string killName = null;
            killName = killerIndex switch
            {
                1 => "OnlyCoins",
                2 => "AllStuff",
                _ => "NoKilling",
            };
            string killerText = this.GetLocalizationKey("KillerType." + killName);
            tooltips.CreateTooltip(killerText, Color.Crimson, "KillerTypeName", killerType + 1);
            tooltips.CreateTooltip(ScarletTextSets.GeneralText_BuffShow, ScarletTextSets.GeneralText_BuffShowColor);
            CacheTooltipList = tooltips;
        }
        public void ExModifyTooltips(List<TooltipLine> tooltips)
        {
            int flavorTooltipIndex2 = tooltips.FindIndex(line => line.Name == "ItemName" && line.Mod == "Terraria");
            string value = "「" + this.GetLocalizedValue("FlavorTooltips").ToLangValue() + "」";
            //实例化toolti并注册名字
            TooltipLine flavorTooltips = new TooltipLine(Mod, "FlavorTooltipsName", value);
            //植入Tooltip
            tooltips.Insert(flavorTooltipIndex2 + 1, flavorTooltips);
        }
        public override void PostDrawTooltipLine(DrawableTooltipLine line)
        {
            if (line.Name == "ItemName" && line.Mod == "Terraria")
            {
                TlipocaRarity.DrawItemName(line);
            }
            if (line.Name == "FlavorTooltipsName" && line.Mod == Mod.Name)
            {
                TlipocaRarity.DrawFlavorTooltipName(line);
            }
            line.ApplyColorForLegendaryData(Color.Red, Color.Black);

            //记录起始点坐标。
            //通常情况下，物品不可能没有名字，而物品名称通常都在第一行，所以可以用这个来记录第一行的坐标
            if (line.IsItemName())
            {
                TextboxManager.FirstLineY = line.Y;
            }
            var settingList = new List<TextboxSettings>();
            if (!Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt))
            {
                string detailText = this.GetLocalizationKey("SpecialAttack").ToLangValue();
                int requirements = Math.Max(0, ExecutionProgress);
                Player p = Main.LocalPlayer;
                int curProgress = p.YardMod().crimsonScytheHitCounter;
                string numberText = Mod.GetLocalizationKey("Database.SpecialAttack.ProgressName").ToLangValue().ToFormatValue(curProgress, requirements);
                detailText += "\n" + "\n" + numberText;
                //一堆设置，巴拉巴拉。
                TextboxSettings sets = new()
                {
                    TitleText = Mod.GetLocalizationKey("Database.SpecialAttack.TitleName").ToLangValue(),
                    TitleTextColor = Color.Lerp(Color.Crimson, Color.Black, 1f) with { A = 255 },
                    TitleEdgeColor = Color.Red,
                    HasTitle = true,
                    BackgroundColor = Color.Lerp(Color.WhiteSmoke, Color.Black, .9f) * .60f,
                    BackgroundEdgeColor = Color.Lerp(Color.White, Color.DarkRed, 1f) * .78f,
                    MainText = detailText,
                    TextColor = Color.White,
                    TextEdgeColor = Color.Black,
                    TitleTextSize = 1.1f,
                    BoxSize = .8f
                };
                settingList.Add(sets);
                //最后传值。

                detailText = this.GetLocalizationKey("ExtraMechanic").ToLangValue();
                sets = new TextboxSettings()
                {
                    TitleText = Mod.GetLocalizationKey("Database.CompanionWeapon").ToLangValue(),
                    TitleTextColor = Color.Lerp(Color.Crimson, Color.Black, 1f) with { A = 255 },
                    TitleEdgeColor = Color.Red,
                    HasTitle = true,
                    BackgroundColor = Color.Lerp(Color.WhiteSmoke, Color.Black, .9f) * .60f,
                    BackgroundEdgeColor = Color.Lerp(Color.White, Color.DarkRed, 1f) * .78f,
                    MainText = detailText,
                    TextColor = Color.White,
                    TextEdgeColor = Color.Black,
                    TitleTextSize = 1.1f,
                    BoxSize = .8f
                };
                settingList.Add(sets);
            }
            else
            {
                settingList.Add(ApplyAbilityTextbox(1).Value);
                settingList.Add(ApplyAbilityTextbox(2).Value);
                settingList.Add(ApplyAbilityTextbox(3).Value);
                settingList.Add(ApplyAbilityTextbox(4).Value);
            }
            TextboxMethods.DrawMultipleTextboxes(line, CacheTooltipList, settingList, 30);
        }
        public TextboxSettings? ApplyAbilityTextbox(int abilityType = 1, params object[] args)
        {
            int curLevel = YardMethods.GetLegendaryLevel();
            int thresholdLevel = (4 * abilityType) - 1;
            string title = Mod.GetLocalizationKey("Database.GenericText.LegendaryAbilityTitle").ToLangValue().ToFormatValue(abilityType, thresholdLevel);
            string mainText = this.GetLocalizationKey("LegendaryAbility.Type" + abilityType).ToLangValue();
            if (args.Length > 0)
                mainText = mainText.ToFormatValue(args);
            float overAllOpac = 1f;
            Color tileEd = Color.Red;
            if (curLevel < thresholdLevel)
            {
                tileEd = Color.Lerp(Color.Red, Color.Black, .3f);
                overAllOpac = .2f;
            }
            TextboxSettings sets = new TextboxSettings
                (
                backgroundColor: Color.Lerp(Color.WhiteSmoke, Color.Black, .9f) * .60f * (.1f + overAllOpac),
                backgroundEdgeColor: Color.Lerp(Color.White, Color.DarkRed, 1f) * .78f * (.1f + overAllOpac),
                mainText: mainText,
                titleText: title,
                textColor: Color.White * overAllOpac,
                textEdgeColor: Color.Black * overAllOpac,
                titleTextColor: Color.Lerp(Color.Crimson, Color.Black, 1f) with { A = 255 } * (.4f + overAllOpac),
                titleEdgeColor: tileEd * overAllOpac,
                titleTextSize: 1.1f,
                hasTitle: true,
                boxSize: .8f
                );
            return sets;
        }

        public override void HoldItem(Player player)
        {
            //player.AddBuff(BuffType<AntiKnockbackBuff>(), 2);
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {

            // 必须手动转换，不然会按照int进行加成
            float Buff = (float)((float)(BaseDamage * CrossModSupprt() + LegendaryDamage()) / (float)BaseDamage);
            damage *= Buff;

        }
        //8个Boss
        public static int LegendaryDamage()
        {
            int applyThis(bool cum, int count)
            {
                return cum ? count : 0;
            }
            int dmgBuff = 0;
            dmgBuff += applyThis(Condition.DownedKingSlime.IsMet(), 5);
            dmgBuff += applyThis(Condition.DownedEyeOfCthulhu.IsMet(), 8);
            dmgBuff += applyThis(Condition.DownedEowOrBoc.IsMet(), 10);
            dmgBuff += applyThis(Condition.DownedQueenBee.IsMet(), 10);
            dmgBuff += applyThis(Condition.DownedSkeletron.IsMet(), 10);
            dmgBuff += applyThis(Main.hardMode, 15);
            dmgBuff += applyThis(Condition.DownedQueenSlime.IsMet(), 20);
            dmgBuff += applyThis(Condition.DownedTwins.IsMet(), 20);
            dmgBuff += applyThis(Condition.DownedDestroyer.IsMet(), 20);
            dmgBuff += applyThis(Condition.DownedSkeletronPrime.IsMet(), 20);
            dmgBuff += applyThis(Condition.DownedPlantera.IsMet(), 30);
            dmgBuff += applyThis(Condition.DownedGolem.IsMet(), 40);
            dmgBuff += applyThis(Condition.DownedEmpressOfLight.IsMet(), 50);
            dmgBuff += applyThis(Condition.DownedDukeFishron.IsMet(), 50);
            dmgBuff += applyThis(Condition.DownedCultist.IsMet(), 100);
            dmgBuff += applyThis(Condition.DownedMoonLord.IsMet(), 200);
            return dmgBuff;
        }
        public static int CrossModSupprt()
        {
            int crossModSupport = 1;
            //开灾厄的情况下基础伤害翻个15倍
            if (ModLoader.HasMod("CalamityMod"))
                crossModSupport += 14;
            if (ModLoader.HasMod("InfernumMode"))
                crossModSupport += 15;
            if (ModLoader.HasMod("ContinentOfJourney"))
                crossModSupport += 1;
            if (ModLoader.HasMod("HJScarletRework"))
                crossModSupport += 1;
            return crossModSupport;
        }
        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.Sickle).
                Register();
        }
    }
}
