namespace O9K.Core.Entities.Abilities.Heroes.Tidehunter
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.tidehunter_anchor_smash)]
    public class AnchorSmash : AreaOfEffectAbility, INuke
    {
        public AnchorSmash(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "attack_damage");
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return base.GetRawDamage(unit, remainingHealth) + this.Owner.GetRawAttackDamage(unit);
        }
    }
}