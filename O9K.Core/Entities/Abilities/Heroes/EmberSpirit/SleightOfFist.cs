namespace O9K.Core.Entities.Abilities.Heroes.EmberSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.ember_spirit_sleight_of_fist)]
    public class SleightOfFist : CircleAbility, INuke
    {
        private readonly SpecialData bonusCreepDamage;

        private readonly SpecialData bonusHeroDamage;

        private bool talentLearned;

        public SleightOfFist(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.bonusHeroDamage = new SpecialData(baseAbility, "bonus_hero_damage");
            this.bonusCreepDamage = new SpecialData(baseAbility, "creep_damage_penalty");
        }

        public override bool IntelligenceAmplify { get; } = false;

        public override bool IsDisplayingCharges
        {
            get
            {
                if (this.talentLearned)
                {
                    return true;
                }

                return this.talentLearned = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_ember_spirit_4)?.Level > 0;
            }
        }

        public override float Radius
        {
            get
            {
                return base.Radius - 50;
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = this.Owner.GetRawAttackDamage(unit);

            if (unit.IsHero)
            {
                damage[this.DamageType] += this.bonusHeroDamage.GetValue(this.Level);
            }
            else if (unit.IsCreep)
            {
                damage[this.DamageType] *= this.bonusCreepDamage.GetValue(this.Level) / -100;
            }

            return damage;
        }
    }
}