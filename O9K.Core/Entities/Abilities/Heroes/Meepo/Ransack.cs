namespace O9K.Core.Entities.Abilities.Heroes.Meepo
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.meepo_ransack)]
    public class Ransack : PassiveAbility, IHasPassiveDamageIncrease
    {
        private readonly SpecialData creepDamageData;

        private readonly SpecialData heroDamageData;

        public Ransack(Ability baseAbility)
            : base(baseAbility)
        {
            this.heroDamageData = new SpecialData(baseAbility, "health_steal_heroes");
            this.creepDamageData = new SpecialData(baseAbility, "health_steal_creeps");
        }

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = string.Empty;

        Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
        {
            var damage = new Damage();

            if (!unit.IsBuilding && !unit.IsAlly(this.Owner) && !this.Owner.IsIllusion)
            {
                damage[this.DamageType] =
                    unit.IsHero ? this.heroDamageData.GetValue(this.Level) : this.creepDamageData.GetValue(this.Level);
            }

            return damage;
        }
    }
}