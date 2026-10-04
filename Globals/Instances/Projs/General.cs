using Terraria;
using Terraria.ModLoader;
using YogsothothsYardMod.Globals.Methods;

namespace YogsothothsYardMod.Globals.Instances.Projs
{
    public partial class YardGlobalProjs : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool FirstFrame = false;
        public bool ExecutionStrike = false;
        public bool HasExecutionMechanic = false;
        public int GlobalTargetIndex = -1;
        public override void AI(Projectile projectile)
        {
            if (!projectile.YardMod().FirstFrame)
            {
                projectile.YardMod().FirstFrame = true;
            }
        }

    }
}
