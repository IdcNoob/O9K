namespace O9K.AIO.Heroes.SkywrathMage.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    internal class MysticFlare : NukeAbility
    {
        public MysticFlare(ActiveAbility ability)
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
            if (target.Speed <= 200)
            {
                return true;
            }

            var immobile = target.GetImmobilityDuration();
            if (immobile < 0.3f)
            {
                var time = (this.Ability.Radius / target.Speed) + 0.3f;
                if (this.Ability.GetDamage(target) * time < target.Health)
                {
                    return false;
                }
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;

            if (this.Owner.HasAghanimsScepter && (target.GetImmobilityDuration() > 0 || !target.IsMoving)
                                              && !targetManager.EnemyHeroes.Any(x => !x.Equals(target) && x.Distance(target) < 900))
            {
                var castPosition = target.InFront(this.Ability.Radius + (target.IsMoving ? 30 : 10));
                if (this.Owner.Distance(castPosition) > this.Ability.CastRange)
                {
                    return false;
                }

                if (!this.Ability.UseAbility(castPosition))
                {
                    return false;
                }
            }
            else
            {
                var input = this.Ability.GetPredictionInput(target);
                input.Delay += 0.3f;
                var output = this.Ability.GetPredictionOutput(input);

                if (output.HitChance < HitChance.Low)
                {
                    return false;
                }

                if (!this.Ability.UseAbility(output.CastPosition))
                {
                    return false;
                }
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }
    }
}