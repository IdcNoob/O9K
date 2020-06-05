namespace O9K.Core.Entities.Abilities.Heroes.Beastmaster
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.beastmaster_wild_axes)]
    public class WildAxes : LineAbility, INuke
    {
        public WildAxes(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "spread");
            this.DamageData = new SpecialData(baseAbility, "axe_damage");
        }

        public override float ActivationDelay { get; } = 0.1f;

        public override float Radius
        {
            get
            {
                return base.Radius * 0.75f;
            }
        }

        public override float Range
        {
            get
            {
                return this.CastRange;
            }
        }

        public override float Speed { get; } = 1200;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return new Damage
            {
                [this.DamageType] = this.DamageData.GetValue(this.Level) * 2
            };
        }
    }
}