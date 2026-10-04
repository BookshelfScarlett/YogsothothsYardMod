using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.Database.Enums;
using YogsothothsYardMod.Core.Huds;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Menus.AltMenu;
using YogsothothsYardMod.Menus.Classes;
using YogsothothsYardMod.Menus.Managers;

namespace YogsothothsYardMod.Menus.MainMenu.Buttons
{
    public class CGs : YardButtonClass
    {
        public override Vector2 ButtonPosition => new Vector2(YardMethods.GetScreenSize.X * .14f, YardMethods.GetScreenSize.Y * .73f) - new Vector2(100, -175);
        public override int TargetMenuID => MenuID.None;
        public override float ButtonScale => .8f;
        public override string HardcodeName => "CGs";
        public override Rectangle Hitbox => Utils.CenteredRectangle(Position, new Vector2(300, 50));
        public static float LerpScaleValue = 1f;
        public static float LerpOpacityValue = 1;

        public override void MouseHover(bool isHover)
        {
            base.MouseHover(isHover);
            if (isHover)
            {
                if (Main.mouseLeft)
                    LerpScaleValue = Lerp(LerpScaleValue, .9f, .2f);
                else
                    LerpScaleValue = Lerp(LerpScaleValue, 1.15f, .2f);
            }
            else
                LerpScaleValue = Lerp(LerpScaleValue, 1f, .2f);

        }

        public override void OnMouseLeftRelease()
        {
            //需要进入alt的二级UI
            //我们需要在这里同时处理两个事项：多人游戏与单人游戏
            //创意工坊按钮已经单独解离出去作为第二按钮，
            if (!YardHudManager.ActiveDepth[2])
            {
                YardMenuUpdates.GeneralFadingRatios = 0;
                CGsHud.IsEnable = true;
                LocalizedText path = Language.GetOrRegister("Mods.YogsothothsYardMod.Menu." + TextKeyName);
                string value = path.Value;
                SoundEngine.PlaySound(YardModSounds.MenuPress);
                YardMenuDraw.DrawTextValue = value;
                YardWaifus randWaifu = Main.rand.NextFromList([YardWaifus.LittleLeaf, YardWaifus.Tlipoca, YardWaifus.XiaLuLing, YardWaifus.Yevna]);
                YardMenuDraw.Waifus = randWaifu;
                int f = Main.rand.NextFromList([0, 2, 4]);
                YardMenuDraw.Useframe = f;
                YardMenuDraw.CurCharactorFrame = f;
                YardMenuDraw.CurPosIndex = Main.rand.Next(0, YardMenuDraw.RandPosList.Count);
                YardMenuDraw.DrawTextValue = value;

            }
        }
        public override void FinalPostUpdate()
        {
            if (Exit.LerpOpacityValue >= .95f)
            {
                if (LerpOpacityValue >= 1f)
                    return;
                LerpOpacityValue = Lerp(LerpOpacityValue, 1f, .12f);
                if (LerpOpacityValue >= .98f)
                    LerpOpacityValue = 1f;

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float lerpingOpcaity = LerpOpacityValue;
            float lerpingScale = LerpScaleValue;
            Texture2D backgroundTexture = YardModAssets.IconBackgoundGlow.Value;
            Texture2D catTexture = YardModAssets.IconCGs.Value;
            DynamicSpriteFont dynamicSpriteFont = YardFonts.Font_YaHei.Value;
            LocalizedText path = Language.GetOrRegister("Mods.YogsothothsYardMod.Menu." + TextKeyName);
            float lerpY = Lerp(75f, 0f, lerpingOpcaity);
            string value = path.Value;
            Vector2 textPos = Position + Vector2.UnitY * lerpY + Vector2.UnitX * 20f;
            Vector2 textSize = ChatManager.GetStringSize(dynamicSpriteFont, value, Vector2.One);

            //目标宽度 = 文本宽度 + 左右各 padding 像素的留白
            float padding = 12f;
            float desiredWidth = textSize.X + padding * 2f;

            //用纹理原始宽度计算缩放，使绘制宽度恰好等于 desiredWidth
            float plateScaleX = desiredWidth / backgroundTexture.Width;
            Vector2 plateScale = new Vector2(plateScaleX, 1f);
            float globalTimer = (float)Math.Sin(Main.timeForVisualEffects * .01f) * 5f;
            Vector2 overAllFloating = Vector2.UnitY * globalTimer;
            //背景
            if (lerpingOpcaity >= .98f)
            {
                spriteBatch.Draw(backgroundTexture, Position+overAllFloating, null, Color.White, 0, new Vector2(0, backgroundTexture.Size().Y / 2f), ButtonScale * LerpScaleValue * plateScale, 0, 0);
                spriteBatch.Draw(catTexture, Position - Vector2.UnitX * 50f+overAllFloating, null, Color.White, 0, catTexture.Size() / 2f, ButtonScale, 0, 0);
            }

            //字体
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);

            ChatManager.DrawColorCodedString(spriteBatch, dynamicSpriteFont, value,
                textPos+overAllFloating, Color.White * lerpingOpcaity, 0, new(0, dynamicSpriteFont.MeasureString(value).Y / 2f), Vector2.One * .55f * lerpingScale);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
            //DrawIconHandler(spriteBatch, LerpScaleValue, LerpOpacityValue);
        }
    }
}
