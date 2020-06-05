namespace O9K.Evader.Abilities.Heroes.Warlock.ShadowWord
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.warlock_shadow_word)]
    internal class ShadowWordBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public ShadowWordBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShadowWordEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}