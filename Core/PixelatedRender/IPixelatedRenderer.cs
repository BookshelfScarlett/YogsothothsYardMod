using Microsoft.Xna.Framework.Graphics;
using YogsothothsYardMod.Core.Database.Enums;

namespace YogsothothsYardMod.Core.PixelatedRender
{
    /// <summary>
    /// 只支持BeforePlayers与BeforeDusts图层
    /// </summary>
    public interface IPixelatedRenderer
    {
        BlendState BlendState => BlendState.AlphaBlend;
        ScarletDrawLayer LayerToRenderTo => ScarletDrawLayer.BeforeDusts;
        void RenderPixelated(SpriteBatch spriteBatch);
    }
}
