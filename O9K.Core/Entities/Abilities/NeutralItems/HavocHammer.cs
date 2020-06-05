namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.item_havoc_hammer)]
    public class HavocHammer : AreaOfEffectAbility, IDebuff, INuke
    {
        private readonly SpecialData damageMultiplierData;

        public HavocHammer(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "range");
            this.DamageData = new SpecialData(baseAbility, "nuke_base_dmg");
            this.damageMultiplierData = new SpecialData(baseAbility, "nuke_str_dmg");
        }

        public override DamageType DamageType { get; } = DamageType.Magical;

        public string DebuffModifierName { get; } = "modifier_havoc_hammer_slow";

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return new Damage
            {
                [this.DamageType] = this.DamageData.GetValue(this.Level)
                                    + (this.damageMultiplierData.GetValue(this.Level) * this.Owner.TotalStrength)
            };
        }
    }
}