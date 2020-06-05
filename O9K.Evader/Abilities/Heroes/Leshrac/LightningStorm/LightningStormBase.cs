namespace O9K.Evader.Abilities.Heroes.Leshrac.LightningStorm
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.leshrac_lightning_storm)]
    internal class LightningStormBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public LightningStormBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new LightningStormEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}