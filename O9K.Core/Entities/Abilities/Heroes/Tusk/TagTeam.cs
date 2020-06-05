namespace O9K.Core.Entities.Abilities.Heroes.Tusk
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.tusk_tag_team)]
    public class TagTeam : AreaOfEffectAbility, IDebuff, IHasPassiveDamageIncrease
    {
        private readonly SpecialData buffDamageData;

        public TagTeam(Ability baseAbility)
            : base(baseAbility)
        {
            this.buffDamageData = new SpecialData(baseAbility, "bonus_damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = "modifier_tusk_tag_team_aura";

        public bool IsPassiveDamagePermanent { get; } = false;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = "modifier_tusk_tag_team_aura";

        Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
        {
            var damage = new Damage();

            if (!unit.IsBuilding && !unit.IsAlly(this.Owner))
            {
                damage[this.DamageType] = this.buffDamageData.GetValue(this.Level);
            }

            return damage;
        }
    }
}