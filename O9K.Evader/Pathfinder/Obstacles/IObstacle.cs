namespace O9K.Evader.Pathfinder.Obstacles
{
    using System.Collections.Generic;

    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    internal interface IObstacle
    {
        bool CanBeDodged { get; }

        Unit9 Caster { get; }

        EvadableAbility EvadableAbility { get; }

        uint Id { get; set; }

        bool IsModifierObstacle { get; }

        bool IsProactiveObstacle { get; }

        Vector3 Position { get; }

        IEnumerable<AbilityId> GetBlinks(Unit9 ally);

        IEnumerable<AbilityId> GetCounters(Unit9 ally);

        int GetDamage(Unit9 ally);

        IEnumerable<AbilityId> GetDisables(Unit9 ally);

        float GetDisableTime(Unit9 enemy);

        float GetEvadeTime(Unit9 ally, bool blink);

        bool IsIntersecting(Unit9 unit, bool checkPrediction);
    }
}