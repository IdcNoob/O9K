namespace O9K.Core.Entities.Abilities.Heroes.TrollWarlord
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.troll_warlord_whirling_axes_ranged)]
    public class WhirlingAxesRanged : ConeAbility, INuke
    {
        public WhirlingAxesRanged(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "axe_width");
            this.SpeedData = new SpecialData(baseAbility, "axe_speed");
            this.DamageData = new SpecialData(baseAbility, "axe_damage");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                // disable casting on target
                return base.AbilityBehavior & ~AbilityBehavior.UnitTarget;
            }
        }

        public override float EndRadius { get; } = 200;
    }
}