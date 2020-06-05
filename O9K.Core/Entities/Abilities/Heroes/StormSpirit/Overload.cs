namespace O9K.Core.Entities.Abilities.Heroes.StormSpirit
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.storm_spirit_overload)]
    public class Overload : PassiveAbility, IHasPassiveDamageIncrease
    {
        public Overload(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "overload_damage");
        }

        public bool IsPassiveDamagePermanent { get; } = false;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = "modifier_storm_spirit_overload";

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage();

            if (!unit.IsBuilding && unit.IsUnit && !unit.IsAlly(this.Owner))
            {
                damage[this.DamageType] = this.DamageData.GetValue(this.Level);
            }

            return damage;
        }
    }
}