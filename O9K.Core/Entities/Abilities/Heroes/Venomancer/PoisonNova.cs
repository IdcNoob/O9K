namespace O9K.Core.Entities.Abilities.Heroes.Venomancer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.venomancer_poison_nova)]
    public class PoisonNova : AreaOfEffectAbility, IDebuff
    {
        public PoisonNova(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public string DebuffModifierName { get; } = "modifier_venomancer_poison_nova";
    }
}