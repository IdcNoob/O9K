namespace O9K.Core.Entities.Abilities.Heroes.VoidSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.void_spirit_dissimilate)]
    public class Dissimilate : AreaOfEffectAbility, IShield
    {
        public Dissimilate(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "phase_duration");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public override float Radius { get; } = 825;

        public string ShieldModifierName { get; } = "modifier_void_spirit_dissimilate_phase";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}