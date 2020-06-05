namespace O9K.Core.Entities.Abilities.Heroes.Pudge
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    using Prediction.Collision;

    [AbilityId(AbilityId.pudge_meat_hook)]
    public class MeatHook : LineAbility, INuke, IDisable
    {
        public MeatHook(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "hook_width");
            this.SpeedData = new SpecialData(baseAbility, "hook_speed");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override bool CanHitSpellImmuneEnemy { get; } = false;

        public override CollisionTypes CollisionTypes { get; } = CollisionTypes.AllUnits;
    }
}