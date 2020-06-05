namespace O9K.Core.Entities.Abilities.Heroes.Pugna
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.pugna_nether_ward)]
    public class NetherWard : CircleAbility, IDebuff
    {
        private readonly SpecialData manaMultiplierData;

        public NetherWard(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.manaMultiplierData = new SpecialData(baseAbility, "mana_multiplier");
        }

        public string DebuffModifierName { get; } = "modifier_pugna_nether_ward_aura";

        public int GetDamage(Unit9 unit, float manaCost)
        {
            var damage = manaCost * this.manaMultiplierData.GetValue(this.Level);
            var amplify = unit.GetDamageAmplification(this.Owner, this.DamageType, false);
            var block = unit.GetDamageBlock(this.DamageType);

            return (int)((damage - block) * amplify);
        }
    }
}