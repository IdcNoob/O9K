namespace O9K.Evader.Abilities.Heroes.Necrophos.DeathPulse
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.necrolyte_death_pulse)]
    internal class DeathPulseBase : EvaderBaseAbility, IEvadable
    {
        public DeathPulseBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DeathPulseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}