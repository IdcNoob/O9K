namespace O9K.AIO.Abilities.Items
{
    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class SpeedBuffAbility : BuffAbility
    {
        public SpeedBuffAbility(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            var target = targetManager.Target;

            if ((!target.IsMoving || target.GetAngle(this.Owner.Position) < 1) && this.Owner.CanAttack(target))
            {
                return false;
            }

            return true;
        }
    }
}