using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Globals.Configs;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Globals.Instances.Items
{
    public class YardGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public bool drawGhostIcon = false;
        public int ghostTimer = 0;
        public int ghostFrame = 1;
        public override void UpdateInventory(Item item, Player player)
        {
            ghostTimer++;
            if (ghostTimer > 5)
            {
                ghostFrame++;
                ghostTimer = 0;
            }
            if (ghostFrame >= 16)
                ghostFrame = 1;
        }
        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (drawGhostIcon && YardModClientConfig.Instance.DrawIcon)
            {
                Vector2 iconPosition = position + new Vector2(15f * Main.inventoryScale, 15f * Main.inventoryScale);
                float iconScale = 0.31f;
                Rectangle rect = new(0, ghostFrame * 44, 46, 42);
                Vector2 recorigin = new(23, 21);
                Texture2D tex = YardModAssets.ScarletGhost.Value;
                for (int i = 0; i < 6; i++)
                    spriteBatch.Draw(tex, iconPosition + ToRadians(60f * i).ToRotationVector2() * 2f, rect, Color.White.ToAddColor(), 0f, recorigin, iconScale, SpriteEffects.None, 0f);
                spriteBatch.Draw(tex, iconPosition, rect, Color.White, 0f, recorigin, iconScale, SpriteEffects.None, 0f);
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            InsertIconInTooltipLine(item, tooltips);
        }
        #region 插入图片，但是在Tooltip内
        //匹配的正则表达式
        private static Regex MatchingBuffIcon = new Regex(@"\[(YardModBuff|YardModDebuff)\/([^\]]+)\]");
        //匹配中间具体Buff的来源和名称的正则表达式
        private static Regex MatchingSpecificBuff = new Regex(@"([^\/]+)\/([^\/]+)");
        //要绘制的Buff列表
        //话说我们为什么要写这么长一串？
        public List<(float, int, string, Texture2D, string, string, string)> buffs = new();
        public List<string> add = new();
        public void GlobalIconInsert(Item item, List<TooltipLine> tooltips)
        {
            //先画出buff转化的提示文本
            //if (buffs.Count != 0)
            //    tooltips.CreateTooltip(ScarletTextSets.GeneralText_BuffShow, ScarletTextSets.GeneralText_BuffShowColor);
            buffs.Clear();
            //遍历tooltip行，我们开始找匹配的正则表达式
            for (int i = 0; i < tooltips.Count; i++)
            {
                Texture2D texture = null;
                string name = string.Empty;
                string descrip = string.Empty;
                float length = 0;
                string color = string.Empty;
                while (MatchingBuffIcon.Match(tooltips[i].Text).Success)
                {
                    tooltips[i].Text = MatchingBuffIcon.Replace(tooltips[i].Text, match =>
                    {
                        return MatchingSpecificBuff.Replace(match.Groups[2].Value, keys =>
                        {
                            color = match.Groups[1].Value switch
                            {
                                "YardModBuff" => "12FFA0",
                                "YardModDebuff" => "FF1150",
                                _ => "FFFFFF"
                            };
                            //获取文本长度，方便定位buff的贴图绘制位置
                            length = ChatManager.GetStringSize(FontAssets.MouseText.Value, tooltips[i].Text.Substring(0, match.Index), Vector2.One).X;
                            //判断一遍原版的buff和mod的buff，两者的buffIcon获取区别很大
                            if (keys.Groups[1].Value == "Terraria")
                            {
                                if (BuffID.Search.TryGetId(keys.Groups[2].Value, out int buffID))
                                {
                                    texture = TextureAssets.Buff[buffID].Value;
                                    name = Lang.GetBuffName(buffID);
                                    descrip = Lang.GetBuffDescription(buffID);
                                    buffs.Add((length, i, color, texture, name, descrip, tooltips[i].Name));
                                }
                            }
                            else
                            {
                                if (ModLoader.TryGetMod(keys.Groups[1].Value, out Mod mod))
                                {
                                    if (mod.TryFind(keys.Groups[2].Value, out ModBuff modBuff))
                                    {
                                        //为啥我们要request啊？有没有别的方案？
                                        texture = Request<Texture2D>(modBuff.Texture).Value;
                                        name = modBuff.DisplayName.Value;
                                        descrip = modBuff.Description.Value;
                                        buffs.Add((length, i, color, texture, name, descrip, tooltips[i].Name));
                                    }
                                }
                            }
                            //计算图标在渲染时应占据的宽度
                            //此时我们不知道最终的行高，但可以基于当前字体计算一个近似比例
                            DynamicSpriteFont currentFont = FontAssets.MouseText.Value;
                            Vector2 currentScale = Vector2.One;
                            //近似行高，用于计算图标宽度
                            float approximateLineHeight = currentFont.MeasureString("M").Y * currentScale.Y;
                            //图标宽高比
                            float iconAspectRatio = (32 + 2) / (float)32;
                            if (texture != null)
                            {
                                iconAspectRatio = (texture.Width + 2) / (float)texture.Height;
                            }

                            //图标应占据的像素宽度
                            float targetWidth = approximateLineHeight * iconAspectRatio;
                            //测量单个空格的宽度
                            float spaceWidth = ChatManager.GetStringSize(currentFont, " ", currentScale).X;
                            //计算所需空格数量
                            int spacesNeeded = (int)Math.Ceiling(targetWidth / spaceWidth);
                            //构建占位空格字符串
                            string placeholder = new string(' ', spacesNeeded);
                            return $"{placeholder}[c/{color}:{name}]";
                        });
                    }, 1);
                }
            }
            //用于写入具体的buffTooltip
            return;
            if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt) && buffs.Count != 0)
            {
                if (tooltips.Count < 2)
                    return;
                tooltips.RemoveRange(1, tooltips.Count - 1);
                for (int j = 0; j < buffs.Count; j++)
                {
                    Texture2D texture = buffs[j].Item4;
                    //计算图标在渲染时应占据的宽度
                    //此时我们不知道最终的行高，但可以基于当前字体计算一个近似比例
                    DynamicSpriteFont currentFont = FontAssets.MouseText.Value;
                    Vector2 currentScale = Vector2.One;
                    //近似行高，用于计算图标宽度
                    float approximateLineHeight = currentFont.MeasureString("M").Y * currentScale.Y;
                    //图标宽高比
                    float iconAspectRatio = (32 + 2) / (float)32;
                    if (texture != null)
                    {
                        iconAspectRatio = (texture.Width + 2) / (float)texture.Height;
                    }
                    //图标应占据的像素宽度
                    float targetWidth = approximateLineHeight * iconAspectRatio;
                    //测量单个空格的宽度
                    float spaceWidth = ChatManager.GetStringSize(currentFont, " ", currentScale).X;
                    //计算所需空格数量
                    int spacesNeeded = (int)Math.Ceiling(targetWidth / spaceWidth);
                    //构建占位空格字符串
                    string placeholder = new string(' ', spacesNeeded);

                    //终于差不多了……加tooltip
                    TooltipLine buffTextNameLine = new TooltipLine(Mod, "YardModIconName" + j, $"{placeholder}[c/{buffs[j].Item3}:{buffs[j].Item5}]");
                    TooltipLine buffTextDescripLine = new TooltipLine(Mod, "YardModDescriptionName" + j, $"{buffs[j].Item6}");
                    tooltips.Add(buffTextNameLine);
                    tooltips.Add(buffTextDescripLine);
                    buffs[j] = (0, tooltips.Count, buffs[j].Item3, buffs[j].Item4, buffs[j].Item5, buffs[j].Item6, buffTextNameLine.Name);
                    if (add.Contains(buffs[j].Item5))
                    {
                        buffs.Remove(buffs[j]);
                        continue;
                    }
                    else
                        add.Add(buffs[j].Item5);
                }

            }
        }
        public void InsertIconInTooltipLine(Item item, List<TooltipLine> tooltips)
        {
            GlobalIconInsert(item, tooltips);
        }
        #endregion
        public override void PostDrawTooltip(Item item, ReadOnlyCollection<DrawableTooltipLine> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                DrawableTooltipLine line = lines[i];
                foreach (var buf in buffs)
                {
                    if (buf.Item7 == line.Name)
                    {
                        DynamicSpriteFont font = line.Font ?? FontAssets.MouseText.Value;
                        Vector2 textScale = line.BaseScale;
                        if (textScale == Vector2.Zero)
                            textScale = Vector2.One;
                        float lineHeight = font.MeasureString("M").Y * textScale.Y;
                        Texture2D icon = buf.Item4;
                        float iconScale = lineHeight / icon.Height;
                        float scaledHeight = icon.Height * iconScale;

                        float x = line.X + buf.Item1 + iconScale * 2f;
                        float y = line.Y + (lineHeight - scaledHeight) - iconScale * 4f;
                        Main.spriteBatch.Draw(buf.Item4, new Vector2(x, y), null, Color.White, 0, Vector2.Zero, iconScale * 1f, 0, 0);
                    }
                    if (buf.Item7 != lines[i].Name)
                        continue;
                }
            }
            add.Clear();
        }
    }
}
