namespace O9K.Core.Entities.Abilities.Heroes.SkywrathMage
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.skywrath_mage_mystic_flare)]
    public class MysticFlare : CircleAbility, INuke
    {
        public MysticFlare(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public override bool HasAreaOfEffect { get; } = false;
    }
}