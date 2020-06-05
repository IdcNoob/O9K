namespace O9K.Evader.Abilities.Heroes.Jakiro.DualBreath
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.jakiro_dual_breath)]
    internal class DualBreathBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public DualBreathBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DualBreathEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}