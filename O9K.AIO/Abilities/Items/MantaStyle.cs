namespace O9K.AIO.Abilities.Items
{
    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class MantaStyle : BuffAbility
    {
        public MantaStyle(ActiveAbility ability)
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

            if (target.IsAttackImmune || this.Owner.Distance(target) > this.Owner.GetAttackRange(target, 200))
            {
                return false;
            }

            return true;
        }
    }
}