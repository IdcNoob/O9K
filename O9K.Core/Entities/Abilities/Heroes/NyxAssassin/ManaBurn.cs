namespace O9K.Core.Entities.Abilities.Heroes.NyxAssassin
{
    using System;

    using Base;
    using Base.Types;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.nyx_assassin_mana_burn)]
    public class ManaBurn : RangedAbility, INuke
    {
        public ManaBurn(Ability baseAbility)
            : base(baseAbility)
        {
            //todo improve cast range

            this.DamageData = new SpecialData(baseAbility, "float_multiplier");
        }

        protected override float BaseCastRange
        {
            get
            {
                if (this.Owner.HasModifier("modifier_nyx_assassin_burrow"))
                {
                    var ability = this.Owner.GetAbilityById(AbilityId.nyx_assassin_burrow);
                    return ability.GetAbilitySpecialData("mana_burn_burrow_range_tooltip");
                }

                return base.BaseCastRange;
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var manaDamage = this.DamageData.GetValue(this.Level);

            return new Damage
            {
                [this.DamageType] = (int)Math.Min(manaDamage * unit.TotalIntelligence, unit.Mana)
            };
        }
    }
}