namespace O9K.AutoUsage.Abilities
{
    using System;

    using Core.Entities.Abilities.Base;

    internal interface IAutoUsageGroup : IDisposable
    {
        void AddAbility(Ability9 ability);

        void RemoveAbility(Ability9 ability);
    }
}