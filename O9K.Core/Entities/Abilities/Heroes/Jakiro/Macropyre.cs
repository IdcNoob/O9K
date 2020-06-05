namespace O9K.Core.Entities.Abilities.Heroes.Jakiro
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.jakiro_macropyre)]
    public class Macropyre : LineAbility
    {
        public Macropyre(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "path_radius");
        }
    }
}