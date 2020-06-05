namespace O9K.Evader.Abilities.Heroes.CrystalMaiden.CrystalNova
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.crystal_maiden_crystal_nova)]
    internal class CrystalNovaBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public CrystalNovaBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new CrystalNovaEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}