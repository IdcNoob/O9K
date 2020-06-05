namespace O9K.Evader.Abilities.Heroes.WitchDoctor.Maledict
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.witch_doctor_maledict)]
    internal class MaledictBase : EvaderBaseAbility, IEvadable
    {
        public MaledictBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MaledictEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}