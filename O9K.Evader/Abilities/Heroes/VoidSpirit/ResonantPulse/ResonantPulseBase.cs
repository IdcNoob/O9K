namespace O9K.Evader.Abilities.Heroes.VoidSpirit.ResonantPulse
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.void_spirit_resonant_pulse)]
    internal class ResonantPulseBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>, IUsable<DisableAbility>
    {
        public ResonantPulseBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ResonantPulseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }

        DisableAbility IUsable<DisableAbility>.GetUsableAbility()
        {
            return new ResonantPulseUsableDisable(this.Ability, this.Menu);
        }
    }
}