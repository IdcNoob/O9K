namespace O9K.Evader.Abilities.Heroes.LegionCommander.OverwhelmingOdds
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.legion_commander_overwhelming_odds)]
    internal class OverwhelmingOddsBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public OverwhelmingOddsBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new OverwhelmingOddsEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}