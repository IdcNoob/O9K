namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_shivas_guard)]
    public class ShivasGuard : AreaOfEffectAbility, IDebuff, INuke
    {
        public ShivasGuard(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "blast_radius");
            this.SpeedData = new SpecialData(baseAbility, "blast_speed");
            this.DamageData = new SpecialData(baseAbility, "blast_damage");
        }

        public override DamageType DamageType { get; } = DamageType.Magical;

        public string DebuffModifierName { get; } = "modifier_item_shivas_guard_blast";
    }
}