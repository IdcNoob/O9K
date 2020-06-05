namespace O9K.Core.Entities.Abilities.Heroes.WitchDoctor
{
    using Base;
    using Base.Components;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.witch_doctor_death_ward)]
    public class DeathWard : CircleAbility, IChanneled
    {
        public DeathWard(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "bounce_radius");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.ChannelTime = baseAbility.GetChannelTime(0);
        }

        public float ChannelTime { get; }

        public bool IsActivatesOnChannelStart { get; } = true;
    }
}