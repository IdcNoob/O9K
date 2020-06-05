namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_buckler)]
    public class Buckler : ToggleAbility
    {
        public Buckler(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "bonus_aoe_radius");
        }
    }
}