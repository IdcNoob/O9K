namespace O9K.AIO.Heroes.Razor.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class StaticLink : DebuffAbility
    {
        public StaticLink(ActiveAbility ability)
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

            if (target.Distance(this.Owner) > 400 && target.GetImmobilityDuration() < 2)
            {
                return false;
            }

            return true;
        }
    }
}