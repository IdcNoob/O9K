namespace O9K.AIO.Heroes.Slark.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class ShadowDance : ShieldAbility
    {
        public ShadowDance(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (this.Owner.HealthPercentage > 30)
            {
                return false;
            }

            return true;
        }
    }
}