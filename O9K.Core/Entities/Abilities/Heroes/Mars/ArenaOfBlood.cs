namespace O9K.Core.Entities.Abilities.Heroes.Mars
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.mars_arena_of_blood)]
    public class ArenaOfBlood : CircleAbility
    {
        public ArenaOfBlood(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "formation_time");
        }
    }
}