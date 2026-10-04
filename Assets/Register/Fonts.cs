using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.ModLoader;

namespace YogsothothsYardMod.Assets.Register
{
    public class YardFonts : ModSystem
    {
        private string Path => "YogsothothsYardMod/Assets/Fonts/";
        public static Asset<DynamicSpriteFont> 等线 { get; private set; }
        public static Asset<DynamicSpriteFont> Font_MGR { get; private set; }
        public static Asset<DynamicSpriteFont> Font_YaHei { get; private set; }
        public override void Load()
        {
            等线 = Request<DynamicSpriteFont>($"{Path}等线");
            Font_MGR = Request<DynamicSpriteFont>($"{Path}Font_MGR");
            Font_YaHei = Request<DynamicSpriteFont>($"{Path}Font_YaHei");
        }

        public override void Unload()
        {
            等线 = null;
            Font_MGR = null;
            Font_YaHei = null;
        }
    }
}
