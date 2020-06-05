namespace O9K.Evader.Abilities.Base.Evadable.Components
{
    using System.Collections.Generic;

    using Core.Entities.Abilities.Base;

    using Ensage;

    internal interface IProactiveCounter
    {
        Ability9 Ability { get; }

        HashSet<AbilityId> ProactiveBlinks { get; }

        HashSet<AbilityId> ProactiveCounters { get; }

        HashSet<AbilityId> ProactiveDisables { get; }

        void AddProactiveObstacle();
    }
}