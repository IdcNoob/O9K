namespace O9K.Evader.Metadata
{
    using System.Collections.Generic;

    using Abilities.Base.Evadable;
    using Abilities.Base.Usable.BlinkAbility;
    using Abilities.Base.Usable.CounterAbility;
    using Abilities.Base.Usable.DisableAbility;
    using Abilities.Base.Usable.DodgeAbility;

    internal interface IAbilityManager
    {
        List<EvadableAbility> EvadableAbilities { get; }

        List<BlinkAbility> UsableBlinkAbilities { get; }

        List<CounterAbility> UsableCounterAbilities { get; }

        List<DisableAbility> UsableDisableAbilities { get; }

        List<DodgeAbility> UsableDodgeAbilities { get; }
    }
}