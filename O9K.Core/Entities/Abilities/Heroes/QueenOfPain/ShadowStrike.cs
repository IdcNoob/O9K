namespace O9K.Core.Entities.Abilities.Heroes.QueenOfPain
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.queenofpain_shadow_strike)]
    public class ShadowStrike : RangedAbility, IDebuff
    {
        public ShadowStrike(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.DamageData = new SpecialData(baseAbility, "strike_damage");
        }

        public string DebuffModifierName { get; } = "modifier_queenofpain_shadow_strike";
    }
}