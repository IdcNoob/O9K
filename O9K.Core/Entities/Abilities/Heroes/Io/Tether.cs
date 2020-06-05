namespace O9K.Core.Entities.Abilities.Heroes.Io
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.wisp_tether)]
    public class Tether : RangedAbility
    {
        public Tether(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "latch_speed");
        }
    }
}