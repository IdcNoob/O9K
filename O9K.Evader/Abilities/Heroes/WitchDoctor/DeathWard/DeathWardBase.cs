namespace O9K.Evader.Abilities.Heroes.WitchDoctor.DeathWard
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.witch_doctor_death_ward)]
    internal class DeathWardBase : EvaderBaseAbility, IEvadable
    {
        public DeathWardBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DeathWardEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}