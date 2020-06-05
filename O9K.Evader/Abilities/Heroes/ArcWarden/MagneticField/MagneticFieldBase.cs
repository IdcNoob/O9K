namespace O9K.Evader.Abilities.Heroes.ArcWarden.MagneticField
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.arc_warden_magnetic_field)]
    internal class MagneticFieldBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public MagneticFieldBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new MagneticFieldUsable(this.Ability, this.Menu);
        }
    }
}