namespace O9K.Evader.Abilities.Items.Satanic
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_satanic)]
    internal class SatanicBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public SatanicBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SatanicEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new SatanicUsable(this.Ability, this.Menu);
        }
    }
}