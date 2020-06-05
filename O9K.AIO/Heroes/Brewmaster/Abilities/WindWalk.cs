namespace O9K.AIO.Heroes.Brewmaster.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class WindWalk : BuffAbility
    {
        public WindWalk(ActiveAbility ability)
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

            if (target.Distance(this.Owner) < 400)
            {
                return false;
            }

            return true;
        }
    }
}