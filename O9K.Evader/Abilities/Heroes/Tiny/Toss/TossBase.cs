namespace O9K.Evader.Abilities.Heroes.Tiny.Toss
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tiny_toss)]
    internal class TossBase : EvaderBaseAbility, IUsable<CounterEnemyAbility>
    {
        public TossBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}