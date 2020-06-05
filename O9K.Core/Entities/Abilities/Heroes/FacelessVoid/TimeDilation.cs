namespace O9K.Core.Entities.Abilities.Heroes.FacelessVoid
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.faceless_void_time_dilation)]
    public class TimeDilation : AreaOfEffectAbility, IDebuff
    {
        public TimeDilation(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = "modifier_faceless_void_time_dilation_slow";
    }
}