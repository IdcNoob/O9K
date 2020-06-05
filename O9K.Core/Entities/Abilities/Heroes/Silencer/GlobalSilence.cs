namespace O9K.Core.Entities.Abilities.Heroes.Silencer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.silencer_global_silence)]
    public class GlobalSilence : AreaOfEffectAbility, IDisable
    {
        public GlobalSilence(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Silenced;

        public override float Radius { get; } = 9999999;
    }
}