namespace O9K.Core.Entities.Abilities.Heroes.ShadowDemon
{
    using System;
    using System.Linq;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.shadow_demon_shadow_poison_release)]
    public class ShadowPoisonRelease : ActiveAbility, INuke
    {
        private SpecialData maxStacksData;

        private SpecialData overflowDamageData;

        private ShadowPoison shadowPoison;

        public ShadowPoisonRelease(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override DamageType DamageType
        {
            get
            {
                return this.shadowPoison.DamageType;
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var stacks = unit.GetModifierStacks("modifier_shadow_demon_shadow_poison");
            if (stacks <= 0)
            {
                return new Damage();
            }

            var lvl = this.Owner.Abilities.FirstOrDefault(x => x.Id == AbilityId.shadow_demon_shadow_poison)?.Level ?? 1;
            var stackDamage = this.DamageData.GetValue(lvl);
            var maxStacks = this.maxStacksData.GetValue(lvl);
            var multiplyStacks = Math.Min(stacks, maxStacks);
            var damage = (int)Math.Pow(2, multiplyStacks - 1) * stackDamage;

            var overflowStacks = Math.Max(stacks - maxStacks, 0);
            if (overflowStacks > 0)
            {
                var overflowDamage = this.overflowDamageData.GetValue(this.Level);
                damage += overflowStacks * overflowDamage;
            }

            return new Damage
            {
                [this.DamageType] = (int)damage
            };
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.shadow_demon_shadow_poison && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.shadowPoison));
            }

            this.shadowPoison = (ShadowPoison)EntityManager9.AddAbility(ability);
            this.DamageData = new SpecialData(this.shadowPoison.BaseAbility, "stack_damage");
            this.maxStacksData = new SpecialData(this.shadowPoison.BaseAbility, "max_multiply_stacks");
            this.overflowDamageData = new SpecialData(this.shadowPoison.BaseAbility, "bonus_stack_damage");
        }
    }
}