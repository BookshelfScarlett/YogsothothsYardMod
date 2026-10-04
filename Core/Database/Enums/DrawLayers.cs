using System;

namespace YogsothothsYardMod.Core.Database.Enums
{
    [Flags]
    public enum ScarletDrawLayer
    {
        BeforeTiles,
        BeforeNPCs,
        BeforeProjectiles,
        BeforePlayer,
        BeforeDusts,
        AfterDusts,
        AfterProjectiles,
        EndCapture
    }
}
