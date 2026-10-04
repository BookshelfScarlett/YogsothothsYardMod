using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Transactions;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.Database.Enums;
using YogsothothsYardMod.Core.Huds;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Menus.AltMenu;
using YogsothothsYardMod.Menus.MainMenu;
using YogsothothsYardMod.Menus.MainMenu.Buttons;

namespace YogsothothsYardMod.Menus.Managers
{
    public class YardMenuDraw
    {
        public static YardHud LoadGame => YardHudManager.UICollection[GetInstance<LoadGames>().Type];
        public static YardHud Multiplayer => YardHudManager.UICollection[GetInstance<Multiplayer>().Type];
        public static YardHud Options => YardHudManager.UICollection[GetInstance<Options>().Type];
        public static YardHud Exit => YardHudManager.UICollection[GetInstance<Exit>().Type];
        public static YardHud Workshop => YardHudManager.UICollection[GetInstance<Workshop>().Type];
        public static YardHud CGs => YardHudManager.UICollection[GetInstance<CGs>().Type];
        public static YardHud CGsHud => YardHudManager.UICollection[GetInstance<CGsHud>().Type];
        public static YardHud SwitchMenu => YardHudManager.UICollection[GetInstance<SwitchMenu>().Type];
        public static YardHud JumpToB2 => YardHudManager.UICollection[GetInstance<JumpToB2>().Type];
        public static YardHud JumpToDC => YardHudManager.UICollection[GetInstance<JumpToDC>().Type];
        public static YardHud JumpToSina => YardHudManager.UICollection[GetInstance<JumpToSina>().Type];
        public static YardHud JumpToX => YardHudManager.UICollection[GetInstance<JumpToX>().Type];

        public static SpriteBatch SB { get => Main.spriteBatch; }
        public static void PreDraw()
        {
            YardMethods.EnterHudArea(BlendState.NonPremultiplied, SamplerState.LinearClamp);
            YardMenuBackground.DrawBackground();
            DrawButton();
            //退出游戏
            SB.End();
            SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
        }
        public static string DrawTextValue = "None";
        public static YardWaifus Waifus = YardWaifus.Tlipoca;
        /// <summary>
        /// 庭院角色小人的帧图
        /// <br>这个用于确定选择哪种样式，结合序列帧去看</br>
        /// <br>正常情况下不能改这个值</br>
        /// </summary>
        public static int Useframe = 1;
        public static int CurCharactorFrame = 1;
        public static int CurTime = 0;
        public static float CurRotation = ToRadians(10f);
        public static int CurPosIndex = 0;
        public static bool IsOpeningAchievement = false;

        public static List<Vector2> RandPosList =
            [
            new Vector2(-600,150),
            new Vector2(-550,-60),
            new Vector2(600,150),
            new Vector2(700,-60),
            new Vector2(700,-60),
            ];
        public static void PostDraw()
        {
            if (Main.menuMode != YardMenu.ID&&!IsOpeningAchievement)
            {
                YardMethods.EnterHudArea(BlendState.NonPremultiplied, SamplerState.LinearClamp);
                DrawBackgroundDirty();
                DrawCharactor();
                SB.End();
                SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            }
        }

        public static void DrawCharactor()
        {
            Texture2D useTex = Waifus switch
            {
                YardWaifus.LittleLeaf => YardModAssets.LittleLeafEmote.Value,
                YardWaifus.Yevna => YardModAssets.YevnaEmote.Value,
                YardWaifus.XiaLuLing => YardModAssets.XiaLuLingEmote.Value,
                _ => YardModAssets.TlipocaEmote.Value,
            };
            Rectangle frame = useTex.Frame(1, 6, 0, CurCharactorFrame);
            CurTime++;
            if (CurTime > 90)
            {
                CurTime = 0;
                CurRotation *= -1;
                CurCharactorFrame += 1;
            }
            if (CurCharactorFrame >= Useframe + 2)
            {
                CurCharactorFrame = Useframe;
            }
            Vector2 pos = YardMethods.GetScreenSize / 2f + new Vector2(600,150);
            SB.Draw(useTex, pos, frame, Color.White, CurRotation, frame.Size() / 2f, .5f, 0, 0);
        }

        public static void DrawBackgroundDirty()
        {
            Vector2 screenSize = YardMethods.GetScreenSize;
            Rectangle rec = Utils.CenteredRectangle(screenSize / 2f, new Vector2(screenSize.X, screenSize.Y));
            //背景
            Texture2D cube = YardModAssets.Texture_WhiteCubeBig.Value;
            SB.Draw(cube, rec, Color.Black * .85f);
            float xBlock = screenSize.X / 2f;
            float yBlock = screenSize.Y + 10;
            float overAllOpac = 1f;
            //画出红色边框
            rec = Utils.CenteredRectangle(screenSize / 2f, new Vector2(xBlock, yBlock));
            SB.Draw(cube, rec, Color.Lerp(Color.DarkRed, Color.Black, .5f) * overAllOpac);
            //画出黑色底图
            rec = Utils.CenteredRectangle(screenSize / 2f, new Vector2(xBlock * .995f, yBlock * .99f));
            SB.Draw(cube, rec, null, Color.Black * overAllOpac);
            Texture2D tex = YardMenu.GetBlackRedGradient(Main.graphics.GraphicsDevice);
            //在此基础上增加黑红渐变
            Vector2 tranSize = new Vector2(screenSize.X / 2f, screenSize.Y);
            rec = Utils.CenteredRectangle(tranSize, new Vector2(xBlock * .995f, yBlock * .99f));
            SB.Draw(tex, rec, null, Color.White * overAllOpac, 0f, new Vector2(0), SpriteEffects.None, 0f);
            //开始画黑线
            Vector2 lineSize = new Vector2(xBlock, screenSize.Y * .1f);
            rec = Utils.CenteredRectangle(lineSize, new Vector2(xBlock, 10));
            SB.Draw(cube, rec, Color.Lerp(Color.DarkRed, Color.Black, .5f) * overAllOpac);
            rec = Utils.CenteredRectangle(lineSize, new Vector2(xBlock, 8));
            SB.Draw(cube, rec, null, Color.Black * overAllOpac);
            //最后，画出字体
            SB.End();
            SB.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            DynamicSpriteFont dynamicSpriteFont = YardFonts.Font_YaHei.Value;
            Vector2 textPos = screenSize / 2 - Vector2.UnitY * 405;
            for (int i = 0; i < 8; i++)
                ChatManager.DrawColorCodedString(SB, dynamicSpriteFont, DrawTextValue,
                    textPos + (TwoPi / 8f * i).ToRotationVector2() * 1.05f, Color.Black, 0, dynamicSpriteFont.MeasureString(DrawTextValue) / 2f, Vector2.One * 1.1f);
            ChatManager.DrawColorCodedString(SB, dynamicSpriteFont, DrawTextValue,
                textPos, Color.White, 0, dynamicSpriteFont.MeasureString(DrawTextValue) / 2f, Vector2.One * 1.1f);
        }
        private static void DrawButton()
        {
            LoadGame.Draw(SB);
            Workshop.Draw(SB);
            Options.Draw(SB);
            SwitchMenu.Draw(SB);
            Multiplayer.Draw(SB);
            Exit.Draw(SB);
            CGs.Draw(SB);
            JumpToX.Draw(SB);
            JumpToSina.Draw(SB);
            JumpToDC.Draw(SB);
            JumpToB2.Draw(SB);
            CGsHud.Draw(SB);
        }
    }
}
