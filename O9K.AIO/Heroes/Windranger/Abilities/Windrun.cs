namespace O9K.AIO.Heroes.Windranger.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class Windrun : ShieldAbility
    {
        public Windrun(ActiveAbility ability)
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

            if (target.IsStunned || target.IsHexed || target.IsDisarmed)
            {
                return false;
            }

            var distance = this.Owner.Distance(target);
            var attackRange = this.Owner.GetAttackRange(target);
            if (distance > attackRange + 100 && distance < attackRange + 500)
            {
                return true;
            }

            if (distance < this.Ability.Radius + 100)
            {
                return true;
            }

            if (target.HealthPercentage < 50)
            {
                return true;
            }

            return false;
        }
    }
}