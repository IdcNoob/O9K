namespace O9K.Core.Entities.Abilities.Heroes.Jakiro
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.jakiro_dual_breath)]
    public class DualBreath : ConeAbility, IDebuff
    {
        public DualBreath(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "start_radius");
            this.EndRadiusData = new SpecialData(baseAbility, "end_radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public string DebuffModifierName { get; } = "modifier_jakiro_dual_breath_slow";
    }
}