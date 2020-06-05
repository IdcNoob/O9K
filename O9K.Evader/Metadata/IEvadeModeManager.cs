namespace O9K.Evader.Metadata
{
    using System.Collections.Generic;

    using Evader.EvadeModes.Modes;

    using Pathfinder.Obstacles;

    internal interface IEvadeModeManager
    {
        IEnumerable<EvadeBaseMode> GetEvadeModes(IObstacle obstacle);
    }
}