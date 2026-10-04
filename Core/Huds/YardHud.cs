using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using YogsothothsYardMod.Core.Handler;
using YogsothothsYardMod.Core.Systems;

namespace YogsothothsYardMod.Core.Huds
{
    public class YardHud : ModType
    {
        public AnimationStruct AniProgress = new AnimationStruct(5);
        public int Type;
        public Vector2 Position;
        public Vector2 Scale;
        public float Scale2;
        public Vector2 Orig;
        public float Rotation;
        public float Opacity = 1f;
        public Color DrawColor;
        public Rectangle Rectangle;
        public bool IsHover;
        public bool IntoHover;
        public bool PressMouseLeft;
        public bool PressMouseRight;
        public bool CanClose = false;
        private static float cachedMaxTextWidth = 0f;
        private static bool textWidthDirty = true;
        /// <summary>
        /// 对齐的方案
        /// <br>-1表示不对齐</br>
        /// </summary>
        public virtual int AlignType => -1;
        public static void MarkTextWidthDirty()
        {
            textWidthDirty = true;
        }
        public static void RefreshMaxTextWidth(List<YardHud> yardHuds, DynamicSpriteFont font, Vector2 scale)
        {
            if (!textWidthDirty)
                return;
            float max = 0f;
            foreach (var e in yardHuds)
            {
                if (e.AlignType == -1)
                    continue;
                string text = e.GetDisplayText();
                if (string.IsNullOrEmpty(text))
                    continue;
                float w = ChatManager.GetStringSize(font, text, scale).X;
                if (w > max)
                    max = w;
            }
            cachedMaxTextWidth = max;
            textWidthDirty = false;
        }
        /// <summary>
        /// 当前共享的最大文本宽度。
        /// </summary>
        public static float SharedMaxTextWidth => cachedMaxTextWidth;

        /// <summary>
        /// 返回本按钮要参与右对齐的文本（默认为空，需要右对齐的派生类重写）。
        /// </summary>
        public virtual string GetDisplayText() => "";
        /// <summary>
        /// 必须分配一个深度
        /// </summary>
        public virtual int UIDepth => 1;
        protected sealed override void Register()
        {
            Type = YardHudManager.UICollection.Count;
            if (!YardHudManager.UICollection.Contains(this))
                YardHudManager.UICollection.Add(this);

            SetDefaults();
        }
        public virtual void SetDefaults()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Scale2 = 1f;
            Orig = Vector2.Zero;
            Rotation = 0f;
            DrawColor = Color.White;
            Rectangle = new Rectangle(0, 0, 0, 0);
            IsHover = false;
        }
        public void Update()
        {
            if (PreSetDepth())
                YardHudManager.ActiveDepthCount[UIDepth] = 2;

            IsHover = Colliding(Rectangle, YardModSystem.MouseRectangle);

            bool CanUpdate = PreUpdateHover() && !YardHudManager.ActiveDepth[UIDepth + 1] && YardHudManager.BlockAllUI == 0;
            if (!CanUpdate)
            {
                IsHover = false;
                IntoHover = false;
            }

            MouseHover(IsHover);

            if (IsHover && !IntoHover)
            {
                StartHover();
                IntoHover = true;
            }
            if (!IsHover && IntoHover)
            {
                OutHover();
                IntoHover = false;
            }

            if (IsHover)
            {
                if (Main.mouseLeft && !PressMouseLeft)
                {
                    OnLeftClick();
                    PressMouseLeft = true;
                }
                if (!Main.mouseLeft && PressMouseLeft)
                {
                    OnMouseLeftRelease();
                    PressMouseLeft = false;
                }
                if (Main.mouseLeft)
                    MouseLeft();

                if (Main.mouseRight && !PressMouseRight)
                {
                    OnRightClick();
                    PressMouseRight = true;
                }
                if (!Main.mouseRight && PressMouseRight)
                {
                    OnMouseRightRelease();
                    PressMouseRight = false;
                }
                if (Main.mouseRight)
                    MouseRight();
            }
            else
            {
                PressMouseLeft = false;
                PressMouseRight = false;
            }

            PostUpdate();
        }
        /// <summary>
        /// 是否更新悬停效果，true为更新，false为完全不更新，null为常驻按照不悬停的模式更新
        /// </summary>
        /// <returns></returns>
        public virtual bool PreUpdateHover()
        {
            return true;
        }
        public virtual bool Colliding(Rectangle rectangle, Rectangle mouseRectangle)
        {
            return rectangle.Contains(Main.MouseScreen.ToPoint());
        }
        /// <summary>
        /// 检测鼠标碰撞
        /// </summary>
        public virtual void MouseHover(bool isHover)
        {
        }
        public virtual void StartHover()
        {
        }
        public virtual void OutHover()
        {
        }
        /// <summary>
        /// 常驻更新
        /// </summary>
        public virtual void PostUpdate()
        {
        }
        public virtual void OnLeftClick()
        {
        }
        public virtual void MouseLeft()
        {
        }
        public virtual void OnRightClick()
        {
        }
        public virtual void MouseRight()
        {
        }
        public virtual void OnMouseLeftRelease()
        {
        }
        public virtual void OnMouseRightRelease()
        {
        }
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
        public virtual bool PreSetDepth()
        {
            return true;
        }
    }
}
