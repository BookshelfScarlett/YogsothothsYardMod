using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace YogsothothsYardMod.Assets.Register
{
    public class Tex2DWithPath
    {
        public Asset<Texture2D> Texture { get; }
        public string Path { get; }
        public Tex2DWithPath(Asset<Texture2D> texture, string path)
        {
            Path = path;
            Texture = texture;
        }
        public Tex2DWithPath(string path)
        {
            Path = path;
            Texture = Request<Texture2D>($"{Path}");
        }
        public Texture2D Value => Texture.Value;
        public int Height => Texture.Height();
        public int Width => Texture.Width();
        public Vector2 Size
        {
            get
            {
                return new Vector2(Width, Height);
            }
        }
        public Vector2 Origin
        {
            get
            {
                return Size / 2;
            }
        }
    }
    public partial class YardModAssets : ModSystem
    {
        private string Path_Menu => $"{TexPath}/Menu/";
        public static Tex2DWithPath BackgroundMain { get; set; }
        public static Tex2DWithPath IconBilibili { get; set; }
        public static Tex2DWithPath IconDiscord { get; set; }
        public static Tex2DWithPath IconQQ { get; set; }
        public static Tex2DWithPath IconWeibo { get; set; }
        public static Tex2DWithPath IconX { get; set; }
        public static Tex2DWithPath IconSelectedBackground { get; set; }
        public static Tex2DWithPath IconBackgoundGlow { get; set; }
        public static Tex2DWithPath IconExits { get; set; }
        public static Tex2DWithPath IconCGs { get; set; }
        public static Tex2DWithPath YardSheet { get; set; }
        public static Tex2DWithPath TlipocaEmote { get; set; }
        public static Tex2DWithPath LittleLeafEmote { get; set; }
        public static Tex2DWithPath YevnaEmote { get; set; }
        public static Tex2DWithPath XiaLuLingEmote { get; set; }
        public static Tex2DWithPath Logo { get; set; }

        public static Tex2DWithPath ButtonBackgroundMain { get; set; }
        public static Tex2DWithPath ButtonBackgroundSelected { get; set; }
        public static Tex2DWithPath CGsButtonSelected { get; set; }
        public static Tex2DWithPath CGsButtonNotSelected { get; set; }
        public override void Load()
        {
            LoadMenu();
            LoadTexture();
            LoadMisc();
            LoadParticle();
            LoadTrail();
        }
        public override void Unload()
        {
            UnloadMenu();
            UnloadTexture();
            UnloadMisc();
            UnLoadParticle();
            UnloadTrail();
        }
        public void LoadMenu()
        {
            BackgroundMain = new Tex2DWithPath(Path_Menu + nameof(BackgroundMain));
            IconBilibili = new Tex2DWithPath(Path_Menu + nameof(IconBilibili));
            IconDiscord = new Tex2DWithPath(Path_Menu + nameof(IconDiscord));
            IconQQ = new Tex2DWithPath(Path_Menu + nameof(IconQQ));
            IconWeibo = new Tex2DWithPath(Path_Menu + nameof(IconWeibo));
            IconX = new Tex2DWithPath(Path_Menu + nameof(IconX));
            IconSelectedBackground = new Tex2DWithPath(Path_Menu + nameof(IconSelectedBackground));
            Logo = new Tex2DWithPath(Path_Menu + nameof(Logo));
            IconBackgoundGlow = new Tex2DWithPath(Path_Menu + nameof(IconBackgoundGlow));
            IconExits = new Tex2DWithPath(Path_Menu + nameof(IconExits));
            IconCGs = new Tex2DWithPath(Path_Menu + nameof(IconCGs));
            YardSheet = new Tex2DWithPath(Path_Menu + nameof(YardSheet));
            TlipocaEmote = new Tex2DWithPath(Path_Menu + nameof(TlipocaEmote));
            LittleLeafEmote = new Tex2DWithPath(Path_Menu + nameof(LittleLeafEmote));
            YevnaEmote = new Tex2DWithPath(Path_Menu + nameof(YevnaEmote));
            XiaLuLingEmote = new Tex2DWithPath(Path_Menu + nameof(XiaLuLingEmote));
            ButtonBackgroundMain = new Tex2DWithPath(Path_Menu + nameof(ButtonBackgroundMain));
            ButtonBackgroundSelected = new Tex2DWithPath(Path_Menu + nameof(ButtonBackgroundSelected));
            CGsButtonNotSelected = new Tex2DWithPath(Path_Menu + nameof(CGsButtonNotSelected));
            CGsButtonSelected = new Tex2DWithPath(Path_Menu + nameof(CGsButtonSelected));
        }
        public void UnloadMenu()
        {
            BackgroundMain = null;
            IconBilibili = null;
            IconDiscord = null;
            IconQQ = null;
            IconWeibo = null;
            IconX = null;
            IconSelectedBackground = null;
            Logo = null;
            ButtonBackgroundMain = null;
            ButtonBackgroundSelected = null;
            IconBackgoundGlow = null;
            TlipocaEmote = null;
            LittleLeafEmote = null;
            XiaLuLingEmote = null;
            YevnaEmote = null;
            IconExits = null;
            IconCGs = null;
            YardSheet = null;
            CGsButtonSelected = null;
            CGsButtonNotSelected = null;
        }
    }
}
