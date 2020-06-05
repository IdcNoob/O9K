namespace O9K.Core.Entities.Abilities.Heroes.Medusa
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.medusa_stone_gaze)]
    public class StoneGaze : AreaOfEffectAbility, IDebuff
    {
        public StoneGaze(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = "modifier_medusa_stone_gaze_slow";
    }
}