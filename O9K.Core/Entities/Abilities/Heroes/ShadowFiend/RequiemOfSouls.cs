namespace O9K.Core.Entities.Abilities.Heroes.ShadowFiend
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.nevermore_requiem)]
    public class RequiemOfSouls : AreaOfEffectAbility
    {
        private readonly SpecialData endRadiusData;

        public RequiemOfSouls(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "requiem_radius");
            this.SpeedData = new SpecialData(baseAbility, "requiem_line_speed");
            this.endRadiusData = new SpecialData(baseAbility, "requiem_line_width_end");
        }

        public override float Radius
        {
            get
            {
                return base.Radius + this.endRadiusData.GetValue(this.Level);
            }
        }
    }
}