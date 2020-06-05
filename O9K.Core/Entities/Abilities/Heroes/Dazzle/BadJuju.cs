namespace O9K.Core.Entities.Abilities.Heroes.Dazzle
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    //todo id
    [AbilityId((AbilityId)7304)]
    public class BadJuju : PassiveAbility
    {
        public BadJuju(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }
    }
}