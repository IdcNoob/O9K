namespace O9K.Core.Entities.Abilities.Heroes.Invoker.Helpers
{
    using System.Collections.Generic;

    using Ensage;

    public interface IInvokableAbility
    {
        bool CanBeInvoked { get; }

        bool IsInvoked { get; }

        AbilityId[] RequiredOrbs { get; }

        bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false);
    }
}