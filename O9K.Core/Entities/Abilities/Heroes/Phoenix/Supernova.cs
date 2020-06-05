namespace O9K.Core.Entities.Abilities.Heroes.Phoenix
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.phoenix_supernova)]
    public class Supernova : AreaOfEffectAbility, IShield
    {
        private readonly SpecialData castRangeData;

        public Supernova(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "aura_radius");
            this.DamageData = new SpecialData(baseAbility, "damage_per_sec");
            this.castRangeData = new SpecialData(baseAbility, "cast_range_tooltip_scepter");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;

                if (this.Owner.HasAghanimsScepter)
                {
                    behavior = (behavior & ~AbilityBehavior.NoTarget) | AbilityBehavior.UnitTarget;
                }

                return behavior;
            }
        }

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public string ShieldModifierName { get; } = "modifier_phoenix_sun";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        protected override float BaseCastRange
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.castRangeData.GetValue(this.Level);
                }

                return 0;
            }
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            var result = this.UnitTargetCast
                             ? this.BaseAbility.UseAbility(this.Owner.BaseUnit, queue, bypass)
                             : this.BaseAbility.UseAbility(queue, bypass);

            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }
    }
}