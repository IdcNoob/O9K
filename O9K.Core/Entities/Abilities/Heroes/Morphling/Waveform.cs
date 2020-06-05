namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.morphling_waveform)]
    public class Waveform : LineAbility, INuke, IBlink
    {
        private readonly SpecialData castRangeData;

        private bool talentLearned;

        public Waveform(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "width");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.castRangeData = new SpecialData(baseAbility, "abilitycastrange");
        }

        public BlinkType BlinkType { get; } = BlinkType.Blink;

        public override bool IsDisplayingCharges
        {
            get
            {
                if (this.talentLearned)
                {
                    return true;
                }

                return this.talentLearned = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_morphling_6)?.Level > 0;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }
    }
}