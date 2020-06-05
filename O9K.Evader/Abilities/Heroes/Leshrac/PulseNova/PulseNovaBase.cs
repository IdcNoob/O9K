namespace O9K.Evader.Abilities.Heroes.Leshrac.PulseNova
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.leshrac_pulse_nova)]
    internal class PulseNovaBase : EvaderBaseAbility, IEvadable
    {
        public PulseNovaBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PulseNovaEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}