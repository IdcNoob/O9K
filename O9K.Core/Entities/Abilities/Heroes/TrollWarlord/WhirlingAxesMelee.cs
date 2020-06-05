namespace O9K.Core.Entities.Abilities.Heroes.TrollWarlord
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.troll_warlord_whirling_axes_melee)]
    public class WhirlingAxesMelee : AreaOfEffectAbility, INuke
    {
        public WhirlingAxesMelee(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "max_range");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }
    }
}