namespace O9K.Core.Entities.Abilities.Heroes.Magnus
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.magnataur_shockwave)]
    public class Shockwave : LineAbility, INuke
    {
        public Shockwave(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "shock_width");
            this.DamageData = new SpecialData(baseAbility, "shock_damage");
            this.SpeedData = new SpecialData(baseAbility, "shock_speed");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                // disable casting on target
                return base.AbilityBehavior & ~AbilityBehavior.UnitTarget;
            }
        }
    }
}