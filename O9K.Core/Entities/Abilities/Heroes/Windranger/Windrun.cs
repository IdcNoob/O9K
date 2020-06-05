namespace O9K.Core.Entities.Abilities.Heroes.Windranger
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.windrunner_windrun)]
    public class Windrun : ActiveAbility, IShield, ISpeedBuff
    {
        private readonly SpecialData bonusMoveSpeedData;

        private bool isInvisibility;

        public Windrun(Ability baseAbility)
            : base(baseAbility)
        {
            this.bonusMoveSpeedData = new SpecialData(baseAbility, "movespeed_bonus_pct");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.AttackImmune;

        public string BuffModifierName { get; } = "modifier_windrunner_windrun";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public override float CastPoint
        {
            get
            {
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

        public override bool IsInvisibility
        {
            get
            {
                if (this.isInvisibility)
                {
                    return true;
                }

                return this.isInvisibility = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_windranger)?.Level > 0;
            }
        }

        public string ShieldModifierName { get; } = "modifier_windrunner_windrun";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return (unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level)) / 100;
        }
    }
}