namespace O9K.Core.Entities.Abilities.Heroes.VoidSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.void_spirit_resonant_pulse)]
    public class ResonantPulse : AreaOfEffectAbility, IShield, INuke, IDisable
    {
        public ResonantPulse(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.RadiusData = new SpecialData(baseAbility, "radius");
            //todo dmg absorb calcs
        }

        public UnitState AppliesUnitState
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return UnitState.Silenced;
                }

                return 0;
            }
        }

        public override bool IsDisplayingCharges
        {
            get
            {
                return this.Owner.HasAghanimsScepter;
            }
        }

        public string ShieldModifierName { get; } = "modifier_void_spirit_resonant_pulse_physical_buff";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}