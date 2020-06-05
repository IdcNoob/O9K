namespace O9K.Evader.Abilities.Heroes.EmberSpirit.FlameGuard
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ember_spirit_flame_guard)]
    internal class FlameGuardBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public FlameGuardBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FlameGuardEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}