namespace O9K.Core.Entities.Abilities.Heroes.Terrorblade
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId((AbilityId)425)]
    public class TerrorWave : AreaOfEffectAbility, IDisable
    {
        public TerrorWave(Ability baseAbility)
            : base(baseAbility)
        {
            //todo fix data
        }

        public override float ActivationDelay { get; } = 0.6f;

        public UnitState AppliesUnitState { get; } = UnitState.Disarmed | UnitState.Silenced | UnitState.Muted;

        public override float Radius { get; } = 1600;

        public override float Speed { get; } = 1000;
    }
}