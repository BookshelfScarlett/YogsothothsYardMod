using Terraria;
using Terraria.ModLoader;

namespace YogsothothsYardMod.Core.Systems
{
    public class YardModSystem : ModSystem
    {
        public static Vector2 ScreenSize = new Vector2(Main.screenWidth, Main.screenHeight);
        public static Rectangle MouseRectangle;
        public override void UpdateUI(GameTime gameTime)
        {
            ScreenSize = new Vector2(Main.screenWidth, Main.screenHeight);
            MouseRectangle = new Rectangle((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 4, 4);
        }
    }
    public partial class YardModKeybings : ModSystem
    {
        /// <summary>
        /// 模组技能键，对于部分盔甲/饰品类技能专用
        /// </summary>
        public static ModKeybind GeneralSkillKeybind { get; private set; }

        public override void Load()
        {
            // Registers a new keybind
            // We localize keybinds by adding a Mods.{ModName}.Keybind.{KeybindName} entry to our localization files. The actual text displayed to English users is in en-US.hjson
            GeneralSkillKeybind = KeybindLoader.RegisterKeybind(Mod, "GenerialSkillKeybind", "T");
        }

        // Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
        public override void Unload()
        {
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            GeneralSkillKeybind = null;
        }
    }
}
