namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_dust)]
    public class DustOfAppearance : AreaOfEffectAbility
    {
        public DustOfAppearance(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }
    }
}