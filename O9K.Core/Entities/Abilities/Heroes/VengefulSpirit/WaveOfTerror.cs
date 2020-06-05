namespace O9K.Core.Entities.Abilities.Heroes.VengefulSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.vengefulspirit_wave_of_terror)]
    public class WaveOfTerror : LineAbility, IDebuff
    {
        public WaveOfTerror(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "wave_width");
            this.SpeedData = new SpecialData(baseAbility, "wave_speed");
        }

        public string DebuffModifierName { get; } = "modifier_vengefulspirit_wave_of_terror";
    }
}