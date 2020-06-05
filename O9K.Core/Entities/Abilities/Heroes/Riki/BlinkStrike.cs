namespace O9K.Core.Entities.Abilities.Heroes.Riki
{
    using System.Linq;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.riki_blink_strike)]
    public class BlinkStrike : RangedAbility, INuke, IBlink
    {
        private readonly SpecialData castRangeData;

        private CloakAndDagger cloakAndDagger;

        public BlinkStrike(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "bonus_damage");
            this.castRangeData = new SpecialData(baseAbility, "abilitycastrange");
        }

        public BlinkType BlinkType { get; } = BlinkType.Targetable;

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);
            var autoAttackDamage = this.Owner.GetRawAttackDamage(unit);

            return damage + autoAttackDamage + this.cloakAndDagger?.GetRawDamage(unit);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.riki_permanent_invisibility && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                return;
            }

            this.cloakAndDagger = (CloakAndDagger)EntityManager9.AddAbility(ability);
        }
    }
}