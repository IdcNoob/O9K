namespace O9K.Evader.Abilities.Heroes.EmberSpirit.SleightOfFist
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ember_spirit_sleight_of_fist)]
    internal class SleightOfFistBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public SleightOfFistBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SleightOfFistEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            //todo better cast position
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}