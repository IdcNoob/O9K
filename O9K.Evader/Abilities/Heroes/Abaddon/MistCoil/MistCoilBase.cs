namespace O9K.Evader.Abilities.Heroes.Abaddon.MistCoil
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.abaddon_death_coil)]
    internal class MistCoilBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public MistCoilBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MistCoilEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}