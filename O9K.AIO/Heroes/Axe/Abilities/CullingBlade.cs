namespace O9K.AIO.Heroes.Axe.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class CullingBlade : NukeAbility
    {
        public CullingBlade(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (targetManager.Target.IsCourier && targetManager.Target.IsVisible && !targetManager.Target.IsInvulnerable)
            {
                return true;
            }

            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            var target = targetManager.Target;
            var targetHp = target.Health + (target.HealthRegeneration * this.Ability.CastPoint) + 15;

            if (this.Ability.GetDamage(target) < targetHp)
            {
                return false;
            }

            return true;
        }
    }
}