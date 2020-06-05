namespace O9K.Evader.Abilities.Heroes.DarkSeer.Vacuum
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dark_seer_vacuum)]
    internal class VacuumBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public VacuumBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new VacuumEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}