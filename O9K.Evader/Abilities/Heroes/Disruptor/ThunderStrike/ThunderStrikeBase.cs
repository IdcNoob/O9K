namespace O9K.Evader.Abilities.Heroes.Disruptor.ThunderStrike
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.disruptor_thunder_strike)]
    internal class ThunderStrikeBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ThunderStrikeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ThunderStrikeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}