namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_ring_of_aquila)]
    public class RingOfAquila : ToggleAbility
    {
        public RingOfAquila(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "aura_radius");
        }
    }
}