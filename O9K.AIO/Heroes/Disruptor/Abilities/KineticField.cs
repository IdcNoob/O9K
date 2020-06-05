namespace O9K.AIO.Heroes.Disruptor.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using SharpDX;

    using TargetManager;

    internal class KineticField : AoeAbility
    {
        public KineticField(ActiveAbility ability)
            : base(ability)
        {
        }

        public Vector3 CastPosition { get; private set; }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var input = this.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            var output = this.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            if (!this.Ability.UseAbility(output.CastPosition))
            {
                return false;
            }

            this.CastPosition = output.CastPosition;
            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public bool UseAbility(Vector3 position, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility(position))
            {
                return false;
            }

            this.CastPosition = position;
            var delay = this.Ability.GetCastDelay(position);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}