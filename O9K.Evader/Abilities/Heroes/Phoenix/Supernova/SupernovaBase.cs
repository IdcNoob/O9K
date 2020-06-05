namespace O9K.Evader.Abilities.Heroes.Phoenix.Supernova
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.phoenix_supernova)]
    internal class SupernovaBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public SupernovaBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SupernovaEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}