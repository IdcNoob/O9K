namespace O9K.Evader.Abilities.Heroes.ShadowDemon.ShadowPoison
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.shadow_demon_shadow_poison)]
    internal class ShadowPoisonBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ShadowPoisonBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShadowPoisonEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}