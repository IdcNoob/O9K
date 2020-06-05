namespace O9K.Core.Entities.Abilities.Heroes.Undying
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.undying_decay)]
    public class Decay : CircleAbility, INuke, IDebuff
    {
        public Decay(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "decay_damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = string.Empty;
    }
}