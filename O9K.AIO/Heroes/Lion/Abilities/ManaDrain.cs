namespace O9K.AIO.Heroes.Lion.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class ManaDrain : TargetableAbility
    {
        public ManaDrain(ActiveAbility ability)
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
            if (target.ManaPercentage < 5)
            {
                return false;
            }

            return target.IsStunned || target.IsHexed || target.IsRooted;
        }
    }
}