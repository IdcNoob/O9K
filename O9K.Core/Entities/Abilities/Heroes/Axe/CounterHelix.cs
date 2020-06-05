namespace O9K.Core.Entities.Abilities.Heroes.Axe
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.axe_counter_helix)]
    public class CounterHelix : PassiveAbility
    {
        public CounterHelix(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}