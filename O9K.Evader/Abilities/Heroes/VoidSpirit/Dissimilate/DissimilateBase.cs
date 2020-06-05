namespace O9K.Evader.Abilities.Heroes.VoidSpirit.Dissimilate
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.void_spirit_dissimilate)]
    internal class DissimilateBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public DissimilateBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DissimilateEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new DissimilateUsable(this.Ability, this.Menu);
        }
    }
}