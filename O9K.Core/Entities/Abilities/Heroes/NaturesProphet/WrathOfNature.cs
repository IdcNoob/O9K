namespace O9K.Core.Entities.Abilities.Heroes.NaturesProphet
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.furion_wrath_of_nature)]
    public class WrathOfNature : RangedAbility, INuke
    {
        private readonly SpecialData scepterDamageData;

        public WrathOfNature(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.scepterDamageData = new SpecialData(baseAbility, "damage_scepter");
        }

        public override float CastRange { get; } = 9999999;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            if (this.Owner.HasAghanimsScepter)
            {
                return new Damage
                {
                    [this.DamageType] = this.scepterDamageData.GetValue(this.Level)
                };
            }

            return base.GetRawDamage(unit, remainingHealth);
        }
    }
}