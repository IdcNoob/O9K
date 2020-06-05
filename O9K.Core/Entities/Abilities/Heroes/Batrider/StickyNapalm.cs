namespace O9K.Core.Entities.Abilities.Heroes.Batrider
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.batrider_sticky_napalm)]
    public class StickyNapalm : CircleAbility, IDebuff
    {
        public StickyNapalm(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = string.Empty;
    }
}