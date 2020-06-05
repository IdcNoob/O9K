namespace O9K.Core.Entities.Abilities.Heroes.Clockwerk
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    using Prediction.Collision;

    [AbilityId(AbilityId.rattletrap_hookshot)]
    public class Hookshot : LineAbility, IDisable
    {
        public Hookshot(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "stun_radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override CollisionTypes CollisionTypes { get; } = CollisionTypes.AllUnits;
    }
}