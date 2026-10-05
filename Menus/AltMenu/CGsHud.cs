using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.Database.Enums;
using YogsothothsYardMod.Core.Huds;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Menus.Managers;

namespace YogsothothsYardMod.Menus.AltMenu
{
    public class CGsHud : YardHud
    {
        public static bool IsEnable = false;
        public static bool IsFading = true;
        public static YardHud Credits => YardHudManager.UICollection[GetInstance<Credits>().Type];
        public static YardHud Achievements => YardHudManager.UICollection[GetInstance<Achievements>().Type];
        public override int UIDepth => 2;
        public override bool PreSetDepth()
        {
            return IsEnable;
        }
        public override void PostUpdate()
        {


            if (Main.keyState.IsKeyDown(Keys.Escape) || Main.mouseRight)
            {
                IsFading = true;
            }
            Rectangle = new Rectangle(0, 0, (int)YardMethods.GetScreenSize.X, (int)YardMethods.GetScreenSize.Y);
            if (IsEnable && !IsFading)
            {
                Opacity = Lerp(Opacity, 1, .2f);
            }
            if (IsFading && (Main.keyState.IsKeyDown(Keys.Escape) || Main.mouseRightRelease))
            {
                Opacity = Lerp(Opacity, 0, 0.2f);
            }
            if (IsEnable)
            {
                Credits.Update();
                Achievements.Update();
            }
            else
            {
                Opacity = Lerp(Opacity, 0, .1f);
                IsFading = false;
            }
            if (Opacity < .02f)
            {
                IsEnable = false;
            }
            Achievements.Opacity = Opacity;
            Credits.Opacity = Opacity;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsEnable)
            {
                YardMethods.EnterHudArea(BlendState.NonPremultiplied, SamplerState.LinearClamp);
                DrawBackgroundDirty(spriteBatch, Opacity);
                DrawCharactor(spriteBatch, Opacity);
                if (!YardMenuDraw.IsOpeningAchievement)
                {
                    Credits.Draw(spriteBatch);
                    Achievements.Draw(spriteBatch);
                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);

            }
        }
        public static void DrawCharactor(SpriteBatch spriteBatch, float opac)
        {
            Texture2D useTex = YardMenuDraw.Waifus switch
            {
                YardWaifus.LittleLeaf => YardModAssets.LittleLeafEmote.Value,
                YardWaifus.Yevna => YardModAssets.YevnaEmote.Value,
                YardWaifus.XiaLuLing => YardModAssets.XiaLuLingEmote.Value,
                _ => YardModAssets.TlipocaEmote.Value,
            };
            Rectangle frame = useTex.Frame(1, 6, 0, YardMenuDraw.CurCharactorFrame);
            YardMenuDraw.CurTime++;
            if (YardMenuDraw.CurTime > 90)
            {
                YardMenuDraw.CurTime = 0;
                YardMenuDraw.CurRotation *= -1;
                YardMenuDraw.CurCharactorFrame += 1;
            }
            if (YardMenuDraw.CurCharactorFrame >= YardMenuDraw.Useframe + 2)
            {
                YardMenuDraw.CurCharactorFrame = YardMenuDraw.Useframe;
            }
            Vector2 pos = YardMethods.GetScreenSize / 2f + new Vector2(600, 150);
            spriteBatch.Draw(useTex, pos, frame, Color.White * opac, YardMenuDraw.CurRotation, frame.Size() / 2f, .5f, 0, 0);
        }

        public static void DrawBackgroundDirty(SpriteBatch SB, float opac)
        {
            Vector2 screenSize = YardMethods.GetScreenSize;
            Rectangle rec = Utils.CenteredRectangle(screenSize / 2f, new Vector2(screenSize.X, screenSize.Y));
            //背景
            Texture2D cube = YardModAssets.Texture_WhiteCubeBig.Value;
            SB.Draw(cube, rec, Color.Black * .85f);
            float xBlock = screenSize.X / 2f;
            if (YardMenuDraw.IsOpeningAchievement)
                xBlock += screenSize.X / 9f;
            float yBlock = screenSize.Y + 10;
            float overAllOpac = 1f * opac;
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
            Vector2 lineSize = new Vector2(screenSize.X / 2f, screenSize.Y * .1f);
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
                ChatManager.DrawColorCodedString(SB, dynamicSpriteFont, YardMenuDraw.DrawTextValue,
                    textPos + (TwoPi / 8f * i).ToRotationVector2() * 1.05f, Color.Black * opac, 0, dynamicSpriteFont.MeasureString(YardMenuDraw.DrawTextValue) / 2f, Vector2.One * 1.1f);
            ChatManager.DrawColorCodedString(SB, dynamicSpriteFont, YardMenuDraw.DrawTextValue,
                textPos, Color.White * opac, 0, dynamicSpriteFont.MeasureString(YardMenuDraw.DrawTextValue) / 2f, Vector2.One * 1.1f);
        }

    }
}
