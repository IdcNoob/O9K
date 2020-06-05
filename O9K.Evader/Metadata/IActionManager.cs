namespace O9K.Evader.Metadata
{
    using System.Collections.Generic;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Pathfinder.Obstacles;

    internal interface IActionManager
    {
        void BlockInput(Unit9 unit, IObstacle obstacle, float seconds);

        void BlockInput(Unit9 unit, float seconds);

        void BlockInput(Ability9 ability, IObstacle obstacle, float seconds);

        void CancelChanneling(Ability9 ability, float timeout = 2);

        IEnumerable<Unit9> GetEvadingObstacleUnits(IObstacle obstacle);

        void IgnoreModifierObstacle(uint abilityHandle, Unit9 unit, float seconds);

        void IgnoreObstacle(IObstacle obstacle, float seconds);

        void IgnoreObstacle(Unit9 unit, IObstacle obstacle, float seconds);

        bool IsInputBlocked(Unit9 unit);

        bool IsObstacleIgnored(Unit9 unit, IObstacle obstacle);

        void UnblockInput(IObstacle obstacle);
    }
}