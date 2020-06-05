namespace O9K.Evader.Abilities.Heroes.Broodmother.SpawnSpiderlings
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.broodmother_spawn_spiderlings)]
    internal class SpawnSpiderlingsBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public SpawnSpiderlingsBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SpawnSpiderlingsEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}