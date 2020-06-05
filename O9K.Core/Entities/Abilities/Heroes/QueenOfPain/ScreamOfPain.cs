namespace O9K.Core.Entities.Abilities.Heroes.QueenOfPain
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.queenofpain_scream_of_pain)]
    public class ScreamOfPain : AreaOfEffectAbility, INuke
    {
        public ScreamOfPain(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
        }
    }
}