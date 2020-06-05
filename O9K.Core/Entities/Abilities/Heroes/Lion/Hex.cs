namespace O9K.Core.Entities.Abilities.Heroes.Lion
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.lion_voodoo)]
    public class Hex : RangedAbility, IDisable, IAppliesImmobility
    {
        public Hex(Ability baseAbility)
            : base(baseAbility)
        {
            //todo radius
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_lion_4);

                if (talent?.Level > 0)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public UnitState AppliesUnitState { get; } = UnitState.Hexed | UnitState.Silenced | UnitState.Disarmed;

        public string ImmobilityModifierName { get; } = "modifier_lion_voodoo";
    }
}