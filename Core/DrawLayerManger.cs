using Terraria;
using Terraria.ModLoader;
using YogsothothsYardMod.Core.MetaballSystem;
using YogsothothsYardMod.Core.ParticleECS;
using YogsothothsYardMod.Core.ParticleSystem;
using YogsothothsYardMod.Core.PixelatedRender;
using YogsothothsYardMod.Core.ScreenEffect;

namespace YogsothothsYardMod.Core
{
    public class DrawLayerManger : ModSystem
    {
        public override void Load()
        {
            //屏幕暗化效果
            On_Main.DrawBackground += ScreenDarknessSystem.DrawScreenDarkness;
            //Metaball层级，可以考虑直接分离出去
            On_Main.DrawDust += MetaballManager.DrawRenderTarget;
            //ECS粒子
            On_Main.DrawDust += ECSParticleDataManager.DrawParticle_ECS;
            //使用类射弹的实例化粒子
            On_Main.DrawDust += BaseParticleManager.DrawParticles;
            //未使用，待删除
            On_Main.DrawPlayers_BehindNPCs += MetaballManager.DrawRenderTargetPiority;
            //未使用，待删除
            On_Main.DrawProjectiles += PixelatedRenderManager.On_Main_DrawProjectiles;
            //像素化渲染
            On_Main.DrawDust += PixelatedRenderManager.DrawTarget_BeforeDust;
            On_Main.DrawPlayers_AfterProjectiles += PixelatedRenderManager.DrawTarget_BeforePlayers;
        }
        public override void Unload()
        {
            //屏幕暗化效果
            On_Main.DrawBackground -= ScreenDarknessSystem.DrawScreenDarkness;
            //Metaball层级，可以考虑直接分离出去
            On_Main.DrawDust -= MetaballManager.DrawRenderTarget;
            //ECS粒子
            On_Main.DrawDust -= ECSParticleDataManager.DrawParticle_ECS;
            //使用类射弹的实例化粒子
            On_Main.DrawDust -= BaseParticleManager.DrawParticles;
            //待删除
            On_Main.DrawPlayers_BehindNPCs -= MetaballManager.DrawRenderTargetPiority;
            //待删除
            On_Main.DrawProjectiles -= PixelatedRenderManager.On_Main_DrawProjectiles;
            //像素化渲染
            On_Main.DrawDust -= PixelatedRenderManager.DrawTarget_BeforeDust;
            On_Main.DrawPlayers_AfterProjectiles -= PixelatedRenderManager.DrawTarget_BeforePlayers;
            //指针绘制
        }
    }
}
