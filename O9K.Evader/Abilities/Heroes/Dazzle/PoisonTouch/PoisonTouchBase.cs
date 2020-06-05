namespace O9K.Evader.Abilities.Heroes.Dazzle.PoisonTouch
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dazzle_poison_touch)]
    internal class PoisonTouchBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public PoisonTouchBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PoisonTouchEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}