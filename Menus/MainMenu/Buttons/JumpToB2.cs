using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Core.Huds;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Menus.MainMenu.Buttons
{
    public class JumpToB2 : YardHud
    {
        public virtual string WebLine => "https://space.bilibili.com/1575718700";
        public override int AlignType => 1;
        public virtual int WebCount => 2;
        public virtual Texture2D IconTex => YardModAssets.IconBilibili.Value;
        public virtual Vector2 IconCenter => YardMethods.GetScreenSize - new Vector2(50, -25 + WebCount * 70);
        public virtual float TextOffsetX => 10;
        public virtual float BackgroundScaleX => 1.5f;
        public float TextOpactiy = 1f;
        public static float PosOffsetY = 1;
        public float IconScale = 1;
        public override string GetDisplayText()
        {
            string localizationKey = "Mods.YogsothothsYardMod.Menu." + GetType().Name;
            string displayText = Language.GetOrRegister(localizationKey).Value;
            return displayText;

        }
        public override void PostUpdate()
        {
            Position = IconCenter;
            Rectangle = Utils.CenteredRectangle(Position, IconTex.Size() * .32f);
            if (JumpToX.PosOffsetY >= .8f)
            {
                if (PosOffsetY >= 1f)
                    return;
                PosOffsetY = Lerp(PosOffsetY, 1f, .12f);
                if (PosOffsetY >= .98f)
                    PosOffsetY = 1f;
            }

        }
        public override void StartHover()
        {
            base.StartHover();
        }
        public override void MouseHover(bool isHover)
        {
            if (isHover)
            {
                TextOpactiy = Lerp(TextOpactiy, 1f, .2f);
                if (Main.mouseLeft)
                    IconScale = Lerp(IconScale, .95f, .2f);
                else
                    IconScale = Lerp(IconScale, 1.15f, .2f);
            }
            else
            {
                TextOpactiy = Lerp(TextOpactiy, 0f, .2f);
                if (TextOpactiy <= .02f)
                    TextOpactiy = 0;
                IconScale = Lerp(IconScale, 1, .2f);
            }
            base.MouseHover(isHover);
        }
        public override void OnLeftClick()
        {
            base.OnLeftClick();
        }
        public override void OnMouseLeftRelease()
        {
            Utils.OpenToURL(WebLine);
            SoundEngine.PlaySound(YardModSounds.MenuPress);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawHandler(spriteBatch, PosOffsetY);
        }
        public void DrawHandler(SpriteBatch spriteBatch, float lerpPosY = 1)
        {
            //绘制图标背景
            Texture2D backgroundTex = IconTex;
            float yOffset = Lerp(45f, 0f, lerpPosY);
            Vector2 vectorHoverOffset = -Vector2.UnitY * yOffset;
            Main.spriteBatch.Draw(backgroundTex, Position + vectorHoverOffset, null, Color.Lerp(Color.OrangeRed, Color.LightGoldenrodYellow, .65f) * lerpPosY, 0, backgroundTex.Size() / 2f, IconScale * .32f, 0, 0);

            //准备本地化文本
            DynamicSpriteFont font = YardFonts.Font_YaHei.Value;
            Vector2 textScale = Vector2.One * .5f;

            Vector2 textSize = ChatManager.GetStringSize(font, GetDisplayText(), textScale);
            //默认右对齐
            Vector2 or = font.MeasureString(GetDisplayText());
            Vector2 textOrigin = new Vector2(or.X, or.Y / 2f);
            //绘制文本栏背景
            float realTextOpacity = TextOpactiy * lerpPosY;
            Vector2 backgroundCenter = IconCenter + new Vector2(-20 * realTextOpacity, 0) - Vector2.UnitX * 20f;
            Texture2D backgroundPlate = YardModAssets.IconSelectedBackground.Value;
            Vector2 plateOrigin = new Vector2(backgroundPlate.Size().X, backgroundPlate.Size().Y / 2);
            Vector2 platePos = backgroundCenter + Vector2.UnitY * 5 - Vector2.UnitX * 5 + vectorHoverOffset;
            Vector2 plateSize = new Vector2(realTextOpacity * BackgroundScaleX, 1);
            float ratios = textSize.X / plateSize.X;
            plateSize = new Vector2(realTextOpacity * ratios, 1);
            //目标宽度 = 文本宽度 + 左右各 padding 像素的留白
            float padding = 12f;
            float desiredWidth = textSize.X + padding * 2f;

            //用纹理原始宽度计算缩放，使绘制宽度恰好等于 desiredWidth
            float plateScaleX = desiredWidth / backgroundPlate.Width;
            Vector2 plateScale = new Vector2(plateScaleX, 1f);
            Main.spriteBatch.Draw(backgroundPlate, platePos, null, Color.White * realTextOpacity, 0, plateOrigin, plateScale, 0, 0);

            //绘制文本与高光方块
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);

            Vector2 textPos = IconCenter + new Vector2(-(30 + TextOffsetX) * realTextOpacity, 5) - Vector2.UnitX * 20f + vectorHoverOffset;
            ChatManager.DrawColorCodedString(Main.spriteBatch, font, GetDisplayText(), textPos, Color.White * realTextOpacity, 0, textOrigin, Vector2.One * .5f);
            //恢复默认批次状态
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);

        }
    }
}
