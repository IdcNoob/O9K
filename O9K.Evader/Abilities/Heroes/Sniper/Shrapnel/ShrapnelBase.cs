namespace O9K.Evader.Abilities.Heroes.Sniper.Shrapnel
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.sniper_shrapnel)]
    internal class ShrapnelBase : EvaderBaseAbility, IUsable<CounterEnemyAbility>
    {
        public ShrapnelBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}