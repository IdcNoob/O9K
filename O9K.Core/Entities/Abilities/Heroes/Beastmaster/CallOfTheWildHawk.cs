namespace O9K.Core.Entities.Abilities.Heroes.Beastmaster
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    //todo id
    [AbilityId((AbilityId)7231)]
    public class CallOfTheWildHawk : CircleAbility
    {
        public CallOfTheWildHawk(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "hawk_vision_tooltip");
            this.SpeedData = new SpecialData(baseAbility, "hawk_speed_tooltip");
        }

        public override float CastRange { get; } = 9999999;
    }
}