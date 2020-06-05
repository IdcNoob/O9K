namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.templar_assassin_meld)]
    public class Meld : RangedAbility, INuke
    {
        public Meld(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "bonus_damage");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                return (base.AbilityBehavior & ~AbilityBehavior.NoTarget) | AbilityBehavior.UnitTarget;
            }
        }

        public override bool BreaksLinkens { get; } = false;

        public override float CastRange
        {
            get
            {
                return this.Owner.GetAttackRange();
            }
        }

        public override bool IntelligenceAmplify { get; } = false;

        public bool IsDispelActive
        {
            get
            {
                //todo change
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_templar_assassin_4);
                if (talent?.Level > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public override bool IsInvisibility { get; } = true;

        public override float Speed
        {
            get
            {
                return this.Owner.ProjectileSpeed;
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);
            var autoAttackDamage = this.Owner.GetRawAttackDamage(unit);

            return damage + autoAttackDamage;
        }

        public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
        {
            //todo queue ?
            var result = this.BaseAbility.UseAbility() && this.Owner.BaseUnit.Attack(target.BaseUnit);

            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }
    }
}