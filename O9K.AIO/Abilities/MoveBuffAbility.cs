namespace O9K.AIO.Abilities
{
    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class MoveBuffAbility : BuffAbility
    {
        public MoveBuffAbility(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (targetManager.Owner.Hero.HasModifier(this.Buff.BuffModifierName))
            {
                return false;
            }

            if (this.Buff is ToggleAbility toggle && toggle.Enabled)
            {
                return false;
            }

            return true;
        }
    }
}