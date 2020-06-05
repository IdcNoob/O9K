namespace O9K.AIO.Heroes.PhantomLancer.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Entity;

    using TargetManager;

    internal class Doppelganger : AoeAbility
    {
        public Doppelganger(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var illusions = EntityManager9.Units.Count(x => x.IsIllusion && x.IsAlly(this.Owner) && x.IsMyControllable && x.IsAlive);
            if (illusions >= 8 && this.Owner.Distance(targetManager.Target) < 300)
            {
                return false;
            }

            var hp = this.Owner.HealthPercentage;
            var target = targetManager.Target;

            if (hp > 75)
            {
                return false;
            }

            if (target.GetImmobilityDuration() > 0)
            {
                return false;
            }

            if (!EntityManager9.EnemyHeroes.Any(x => x.Distance(this.Owner) < 1000 && x.GetAngle(this.Owner) < 1))
            {
                return false;
            }

            return true;
        }

        //public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        //{
        //    if (!this.Ability.UseAbility(targetManager.Target.GetPredictedPosition(0.5f)))
        //    {
        //        return false;
        //    }

        //    var delay = this.Ability.GetCastDelay(targetManager.Target);
        //    comboSleeper.Sleep(delay);
        //    this.Sleeper.Sleep(delay + 0.5f);
        //    this.OrbwalkSleeper.Sleep(delay);
        //    return true;
        //}
    }
}