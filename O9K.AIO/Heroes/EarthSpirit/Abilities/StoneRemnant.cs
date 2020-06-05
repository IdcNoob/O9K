namespace O9K.AIO.Heroes.EarthSpirit.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    internal class StoneRemnant : TargetableAbility
    {
        public StoneRemnant(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility(this.Owner.Position.Extend2D(targetManager.Target.Position, 100)))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay + 0.1f);
            this.Sleeper.Sleep(1);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var modifier = target.GetModifier("modifier_earth_spirit_magnetize");

            if (modifier == null || modifier.RemainingTime > 0.75f)
            {
                return false;
            }

            var targets = targetManager.EnemyHeroes.Where(x => x.HasModifier("modifier_earth_spirit_magnetize")).ToList();

            var input = this.Ability.GetPredictionInput(target, targets);
            input.Radius = 400;
            input.AreaOfEffect = true;
            input.SkillShotType = SkillShotType.Circle;
            var output = this.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            if (!this.Ability.UseAbility(output.CastPosition))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay + 0.1f);
            this.Sleeper.Sleep(1);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, BoulderSmash smash)
        {
            var target = targetManager.Target;
            var input = smash.Ability.GetPredictionInput(target);
            var output = smash.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            if (!this.Ability.UseAbility(this.Owner.Position.Extend2D(output.CastPosition, 100)))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay + 0.1f);
            this.Sleeper.Sleep(1);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, GeomagneticGrip grip)
        {
            var target = targetManager.Target;
            var input = grip.Ability.GetPredictionInput(target);
            var output = grip.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            if (!this.Ability.UseAbility(output.CastPosition))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay + 0.1f);
            this.Sleeper.Sleep(1);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}