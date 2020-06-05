namespace O9K.Core.Entities.Abilities.Heroes.Slark
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.slark_dark_pact)]
    public class DarkPact : AreaOfEffectAbility, IShield
    {
        public DarkPact(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "delay");
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "total_damage");
        }

        public UnitState AppliesUnitState { get; } = 0;

        public string ShieldModifierName { get; } = "modifier_slark_dark_pact";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        public override float GetHitTime(Unit9 unit)
        {
            if (this.Owner.Equals(unit))
            {
                return this.GetCastDelay();
            }

            return this.GetHitTime(unit.Position);
        }
    }
}