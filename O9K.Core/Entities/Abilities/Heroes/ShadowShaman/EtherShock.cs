namespace O9K.Core.Entities.Abilities.Heroes.ShadowShaman
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.shadow_shaman_ether_shock)]
    public class EtherShock : RangedAreaOfEffectAbility, INuke
    {
        public EtherShock(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "start_radius");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }
    }
}