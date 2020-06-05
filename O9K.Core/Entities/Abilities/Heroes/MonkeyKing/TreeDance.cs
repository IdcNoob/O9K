namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.monkey_king_tree_dance)]
    public class TreeDance : RangedAbility, IBlink
    {
        public TreeDance(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "leap_speed");
        }

        public BlinkType BlinkType { get; } = BlinkType.Targetable;

        public override bool TargetsEnemy { get; } = false;
    }
}