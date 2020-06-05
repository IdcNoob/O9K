namespace O9K.Core.Entities.Abilities.Heroes.Meepo
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.meepo_poof)]
    public class Poof : RangedAbility
    {
        public Poof(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "poof_damage");
        }

        public override float CastRange { get; } = 9999999;
    }
}