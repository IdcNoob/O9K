namespace O9K.Core.Entities.Abilities.Heroes.AncientApparition
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.ancient_apparition_cold_feet)]
    public class ColdFeet : RangedAbility, IDebuff
    {
        public ColdFeet(Ability baseAbility)
            : base(baseAbility)
        {
            //todo better talent aoe calcs
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_ancient_apparition_6);

                if (talent?.Level > 0)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public string DebuffModifierName { get; } = "modifier_cold_feet";
    }
}