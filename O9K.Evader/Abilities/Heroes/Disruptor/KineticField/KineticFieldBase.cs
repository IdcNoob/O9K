namespace O9K.Evader.Abilities.Heroes.Disruptor.KineticField
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.disruptor_kinetic_field)]
    internal class KineticFieldBase : EvaderBaseAbility, IEvadable
    {
        public KineticFieldBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new KineticFieldEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}