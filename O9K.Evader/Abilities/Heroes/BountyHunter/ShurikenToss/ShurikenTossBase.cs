namespace O9K.Evader.Abilities.Heroes.BountyHunter.ShurikenToss
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bounty_hunter_shuriken_toss)]
    internal class ShurikenTossBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ShurikenTossBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShurikenTossEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}