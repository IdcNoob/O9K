namespace O9K.AIO.Heroes.AntiMage.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;

    using TargetManager;

    internal class AntiMageBlink : BlinkAbility
    {
        public AntiMageBlink(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var attackRange = this.Owner.GetAttackRange(target);
            var distance = target.Distance(this.Owner);

            if (distance <= attackRange + 100)
            {
                return false;
            }

            if (distance <= attackRange + 250 && this.Owner.Speed > targetManager.Target.Speed + 50)
            {
                return false;
            }

            var position = target.GetAngle(this.Owner) < 1
                               ? target.Position.Extend2D(EntityManager9.EnemyFountain, 100)
                               : target.GetPredictedPosition(this.Ability.CastPoint + 0.3f);

            if (this.Owner.Distance(position) > this.Ability.CastRange)
            {
                return false;
            }

            if (!this.Ability.UseAbility(position))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}