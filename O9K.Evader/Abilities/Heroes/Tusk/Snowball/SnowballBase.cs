namespace O9K.Evader.Abilities.Heroes.Tusk.Snowball
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tusk_snowball)]
    internal class SnowballBase : EvaderBaseAbility, /* IEvadable,*/ IUsable<CounterAbility>
    {
        public SnowballBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SnowballEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            //todo better target selection
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}