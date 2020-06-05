namespace O9K.Evader.Abilities.Heroes.Beastmaster.WildAxes
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.beastmaster_wild_axes)]
    internal class WildAxesBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public WildAxesBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WildAxesEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}