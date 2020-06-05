namespace O9K.Core.Entities.Abilities.Heroes.PhantomAssassin
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.phantom_assassin_phantom_strike)]
    public class PhantomStrike : RangedAbility, IBlink, INuke
    {
        public PhantomStrike(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public BlinkType BlinkType { get; } = BlinkType.Targetable;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return this.Owner.GetRawAttackDamage(unit);
        }
    }
}