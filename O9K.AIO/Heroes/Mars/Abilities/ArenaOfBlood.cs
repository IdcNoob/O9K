namespace O9K.AIO.Heroes.Mars.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    internal class ArenaOfBlood : AoeAbility
    {
        public ArenaOfBlood(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var castRange = this.Ability.CastRange;

            for (var i = 50; i <= castRange; i += 50)
            {
                var input = this.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
                input.CastRange = i;
                input.Radius -= 125;
                var output = this.Ability.GetPredictionOutput(input);

                if (output.HitChance < HitChance.Low)
                {
                    continue;
                }

                if (!this.Ability.UseAbility(output.CastPosition))
                {
                    continue;
                }

                var delay = this.Ability.GetCastDelay(targetManager.Target);
                comboSleeper.Sleep(delay);
                this.Sleeper.Sleep(delay + 0.5f);
                this.OrbwalkSleeper.Sleep(delay);
                return true;
            }

            return false;
        }
    }
}