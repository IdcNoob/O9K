namespace O9K.AIO.Heroes.LegionCommander.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using TargetManager;

    internal class OverwhelmingOdds : NukeAbility
    {
        public OverwhelmingOdds(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (aoe)
            {
                var input = this.Ability.GetPredictionInput(
                    targetManager.Target,
                    EntityManager9.Units.Where(
                            x => x.IsUnit && !x.IsAlly(this.Owner) && x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable)
                        .ToList());
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
            else
            {
                if (!this.Ability.UseAbility(targetManager.Target, HitChance.Low))
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