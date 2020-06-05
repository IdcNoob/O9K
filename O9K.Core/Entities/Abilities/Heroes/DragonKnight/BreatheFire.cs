namespace O9K.Core.Entities.Abilities.Heroes.DragonKnight
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.dragon_knight_breathe_fire)]
    public class BreatheFire : ConeAbility, INuke
    {
        public BreatheFire(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "start_radius");
            this.EndRadiusData = new SpecialData(baseAbility, "end_radius");
            this.RangeData = new SpecialData(baseAbility, "range");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                // disable casting on target
                return base.AbilityBehavior & ~AbilityBehavior.UnitTarget;
            }
        }

        public override bool BreaksLinkens { get; } = false;
    }
}