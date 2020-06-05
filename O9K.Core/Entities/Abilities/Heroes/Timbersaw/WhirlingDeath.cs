namespace O9K.Core.Entities.Abilities.Heroes.Timbersaw
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.shredder_whirling_death)]
    public class WhirlingDeath : AreaOfEffectAbility, INuke
    {
        public WhirlingDeath(Ability baseAbility)
            : base(baseAbility)
        {
            //todo tree cut + attribute reduction damage ?
            this.RadiusData = new SpecialData(baseAbility, "whirling_radius");
            this.DamageData = new SpecialData(baseAbility, "whirling_damage");
        }
    }
}