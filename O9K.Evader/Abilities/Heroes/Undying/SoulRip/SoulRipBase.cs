namespace O9K.Evader.Abilities.Heroes.Undying.SoulRip
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.undying_soul_rip)]
    internal class SoulRipBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public SoulRipBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SoulRipEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}