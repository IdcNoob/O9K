namespace O9K.Evader.Abilities.Heroes.Zeus.ArcLightning
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.zuus_arc_lightning)]
    internal class ArcLightningBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ArcLightningBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ArcLightningEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}