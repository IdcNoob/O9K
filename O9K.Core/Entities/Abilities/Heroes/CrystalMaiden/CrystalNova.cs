namespace O9K.Core.Entities.Abilities.Heroes.CrystalMaiden
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.crystal_maiden_crystal_nova)]
    public class CrystalNova : CircleAbility, INuke, IDebuff
    {
        public CrystalNova(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "nova_damage");
        }

        public string DebuffModifierName { get; } = "modifier_crystal_maiden_crystal_nova";
    }
}