namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
    using System.Linq;

    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.spectre_desolate)]
    public class Desolate : PassiveAbility, IHasPassiveDamageIncrease
    {
        private readonly SpecialData radiusData;

        public Desolate(Ability baseAbility)
            : base(baseAbility)
        {
            this.radiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "bonus_damage");
        }

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = string.Empty;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage();

            if (!unit.IsBuilding && !unit.IsAlly(this.Owner) && !EntityManager9.Units.Any(
                    x => x.IsUnit && !x.Equals(unit) && !x.IsInvulnerable && x.IsAlive && x.IsVisible && x.IsAlly(unit)
                         && x.Distance(unit) < this.radiusData.GetValue(this.Level)))
            {
                damage[this.DamageType] = this.DamageData.GetValue(this.Level);
            }

            return damage;
        }
    }
}