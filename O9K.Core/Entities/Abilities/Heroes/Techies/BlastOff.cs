namespace O9K.Core.Entities.Abilities.Heroes.Techies
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.techies_suicide)]
    public class BlastOff : CircleAbility
    {
        public BlastOff(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "duration");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }
    }
}