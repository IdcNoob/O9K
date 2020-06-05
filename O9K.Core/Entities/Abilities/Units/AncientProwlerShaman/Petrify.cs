namespace O9K.Core.Entities.Abilities.Units.AncientProwlerShaman
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.spawnlord_master_freeze)]
    public class Petrify : OrbAbility, IDisable, IAppliesImmobility
    {
        public Petrify(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Rooted;

        public override float CastRange
        {
            get
            {
                return base.CastRange + 100;
            }
        }

        public string ImmobilityModifierName { get; } = "modifier_spawnlord_master_freeze_root";
    }
}