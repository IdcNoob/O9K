namespace O9K.AIO.Heroes.Warlock.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using TargetManager;

    internal class FatalBonds : DebuffAbility
    {
        public FatalBonds(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var targets = EntityManager9.Units.Where(x => x.IsAlive && x.IsVisible && !x.IsAlly(this.Owner) && !x.IsInvulnerable);
            var input = this.Ability.GetPredictionInput(targetManager.Target, targets.ToList());
            var output = this.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low || output.AoeTargetsHit.Count < 3)
            {
                return false;
            }

            if (!this.Ability.UseAbility(output.Target))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}