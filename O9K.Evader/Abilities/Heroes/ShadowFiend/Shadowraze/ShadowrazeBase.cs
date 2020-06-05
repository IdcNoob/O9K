namespace O9K.Evader.Abilities.Heroes.ShadowFiend.Shadowraze
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.nevermore_shadowraze1)]
    [AbilityId(AbilityId.nevermore_shadowraze2)]
    [AbilityId(AbilityId.nevermore_shadowraze3)]
    internal class ShadowrazeBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ShadowrazeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShadowrazeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}