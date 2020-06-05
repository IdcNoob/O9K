namespace O9K.Evader.Abilities.Heroes.SkywrathMage.ConcussiveShot
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.skywrath_mage_concussive_shot)]
    internal class ConcussiveShotBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ConcussiveShotBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ConcussiveShotEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}