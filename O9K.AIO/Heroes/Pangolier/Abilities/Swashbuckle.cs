namespace O9K.AIO.Heroes.Pangolier.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    using BaseSwashbuckle = Core.Entities.Abilities.Heroes.Pangolier.Swashbuckle;

    internal class Swashbuckle : NukeAbility
    {
        private readonly BaseSwashbuckle swashbuckle;

        public Swashbuckle(ActiveAbility ability)
            : base(ability)
        {
            this.swashbuckle = (BaseSwashbuckle)ability;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (this.Owner.HasModifier("modifier_pangolier_gyroshell"))
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var input = this.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            input.CastRange = this.Ability.CastRange;
            input.Range = this.Ability.Range;
            input.UseBlink = true;
            input.AreaOfEffect = true;
            var output = this.Ability.GetPredictionOutput(input);
            if (output.HitChance < HitChance.Low || this.Owner.Distance(output.TargetPosition) > this.Ability.CastRange + 100)
            {
                return false;
            }

            if (!this.swashbuckle.UseAbility(output.BlinkLinePosition, output.CastPosition))
            {
                return false;
            }

            var delay = this.Ability.GetHitTime(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}