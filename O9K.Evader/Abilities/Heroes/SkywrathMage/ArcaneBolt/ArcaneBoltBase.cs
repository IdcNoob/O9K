namespace O9K.Evader.Abilities.Heroes.SkywrathMage.ArcaneBolt
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.skywrath_mage_arcane_bolt)]
    internal class ArcaneBoltBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ArcaneBoltBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ArcaneBoltEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}