using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using YogsothothsYardMod.Assets.Register;
using YogsothothsYardMod.Content.Items.Weapon;
using YogsothothsYardMod.Globals.Configs;
using YogsothothsYardMod.Globals.Methods;
using YogsothothsYardMod.Globals.Players;

namespace HJScarletRework.Globals.Huds
{
    public class ExecutionHudManager : ModSystem
    {
        public static float GeneralOpactiy = 0;
        public static float GeneralOffset = 0;
        public static bool DrawFadeOut;
        public static bool DrawFadeIn;
        public static int StopCounter = 0;
        /// <summary>
        /// Shorthand.
        /// </summary>
        public Player LocalPlayer => Main.LocalPlayer;
        public YardPlayer ModPlayer => LocalPlayer.YardMod();
        public static bool FadeInCounter = false;
        public static float GeneralOpacity = 0;
        public static Vector2 TargetSize = new Vector2(600, 600);
        /// <summary>
        /// 仅仅用于计时
        /// </summary>
        public static int CurExecutionCounter = -1;
        public override void Load()
        {
            if (Main.dedServ)
                return;
            On_Main.DrawDust += On_Main_DrawDust;
            LoadInit();

        }

        private void On_Main_DrawDust(On_Main.orig_DrawDust orig, Main self)
        {
            orig(self);
            if (Main.dedServ || Main.gameMenu)
                return;
            YardModClientConfig config = YardModClientConfig.Instance;
            if (!config.DrawExecutionCounter)
                return;

            Player localPlayer = Main.LocalPlayer;
            if (GeneralOpacity <= 0f && !localPlayer.YardMod().Executor_DrawFadeIn)
                return;
            if (LocalPlayer.dead)
                return;
            SpriteBatch SB = Main.spriteBatch;
            Vector2 pos = LocalPlayer.Center + new Vector2(0, 50) - Main.screenPosition;
            pos.Y += LocalPlayer.gfxOffY;
            Texture2D t2d = YardModAssets.ButtonBackgroundSelected.Value;
            SB.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            SB.Draw(t2d, pos, null, Color.Red * GeneralOpacity, 0f, t2d.Size() / 2f, 1.2f, SpriteEffects.None, 0f);
            DrawNumberWithEffect(SB, pos, CurExecutionCounter, GeneralOpacity);

            SB.End();

        }
        private void DrawNumberWithEffect(SpriteBatch sb, Vector2 basePos, int number, float opa)
        {
            string numStr = number.ToString();
            DynamicSpriteFont font = YardFonts.Font_MGR.Value;
            Vector2 scale = new Vector2(.6f);
            float offsetX = GetNumberOffsetX(number);
            Vector2 size = ChatManager.GetStringSize(font, numStr, scale);
            Vector2 textPos = basePos - new Vector2(offsetX, 0);

            Color shadowColor1 = Color.Lerp(Color.Red, Color.Black, 0.95f) * (opa * 0.248f);

            Color shadowColor2 = Color.Black * opa;
            Color mainColor = Color.White * opa;
            for (int i = 0; i < 8; i++)
            {
                Vector2 offset = (TwoPi * i / 8f).ToRotationVector2() * 1.2f;

                //第一层阴影
                Vector2 pos1 = textPos + offset + new Vector2(3.5f, 3.5f);
                ChatManager.DrawColorCodedString(sb, font, numStr, pos1, shadowColor1, 0f, size * 0.5f, scale);

                //第二层阴影
                Vector2 pos2 = textPos + offset;
                ChatManager.DrawColorCodedString(sb, font, numStr, pos2, shadowColor2, 0f, size * 0.5f, scale);
            }

            //中心白色文字
            ChatManager.DrawColorCodedString(sb, font, numStr, textPos, mainColor, 0f, size * 0.5f, scale);

        }
        private float GetNumberOffsetX(int number)
        {
            if (number < 10)
                return 5f;
            if (number < 100)
                return 7.30f;
            if (number < 1000)
                return 15f;
            if (number < 10000)
                return 21f;
            return 21f;
        }

        public override void Unload()
        {
            if (Main.dedServ)
                return;
            On_Main.DrawDust -= On_Main_DrawDust;

        }
        public override void OnWorldLoad()
        {
            LoadInit();
        }
        public static void LoadInit()
        {
            GeneralOpactiy = 0;
        }
        public override void UpdateUI(GameTime gameTime)
        {
            YardModClientConfig config = YardModClientConfig.Instance;
            if (Main.dedServ)
                return;
            if (!config.DrawExecutionCounter)
                return;

            Player localPlayer = Main.LocalPlayer;
            Item heldItem = localPlayer.HeldItem;
            if (!heldItem.IsLegal())
                return;

            if (heldItem.type == ItemType<CrimsonScythe>())
            {
                if (!ModPlayer.Executor_DrawFadeIn)
                {
                    ModPlayer.Executor_DrawFadeOut = false;
                    ModPlayer.Executor_DrawFadeIn = true;
                }
            }
            else
            {
                if (!ModPlayer.Executor_DrawFadeOut)
                {
                    ModPlayer.Executor_DrawFadeOut = true;
                    ModPlayer.Executor_DrawFadeIn = false;
                }
            }
            if (ModPlayer.Executor_DrawFadeOut)
            {
                GeneralOpacity = Lerp(GeneralOpacity, 0, 0.12f);
                GeneralOffset = Lerp(GeneralOffset, -5f, 0.2f);
                if (GeneralOpacity <= .02f)
                {
                    GeneralOpacity = 0f;
                    GeneralOffset = 0f;
                    ModPlayer.Executor_DrawFadeOut = false;
                }
            }
            else if (ModPlayer.Executor_DrawFadeIn)
            {
                GeneralOpacity = Lerp(GeneralOpacity, 1f, 0.2f);
                GeneralOffset = Lerp(GeneralOffset, 0f, 0.2f);
                if (GeneralOpacity >= 0.98f)
                {
                    GeneralOpacity = 1f;
                    GeneralOffset = 0f;
                }
            }
            //可见度为0的时候停止下方的更新
            if (GeneralOpacity <= 0f && !ModPlayer.Executor_DrawFadeIn)
                return;

            //发射的一瞬间会从字典移除掉对应的值
            //如果最开始的值就不存在，设置为0
            CurExecutionCounter = ModPlayer.crimsonScytheHitCounter;
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
        }
    }
}
