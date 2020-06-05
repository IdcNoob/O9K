namespace O9K.Core.Entities.Abilities.Heroes.StormSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.storm_spirit_static_remnant)]
    public class StaticRemnant : AreaOfEffectAbility, INuke
    {
        public StaticRemnant(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "static_remnant_radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "static_remnant_delay");
            this.DamageData = new SpecialData(baseAbility, "static_remnant_damage");
        }
    }
}