namespace O9K.Evader.Abilities.Heroes.Dazzle.ShallowGrave
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dazzle_shallow_grave)]
    internal class ShallowGraveBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public ShallowGraveBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShallowGraveEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            //todo improve usage vs axe ult
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}