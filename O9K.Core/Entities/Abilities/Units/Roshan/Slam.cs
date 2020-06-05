namespace O9K.Core.Entities.Abilities.Units.Roshan
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.roshan_slam)]
    public class Slam : AreaOfEffectAbility
    {
        public Slam(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }
    }
}