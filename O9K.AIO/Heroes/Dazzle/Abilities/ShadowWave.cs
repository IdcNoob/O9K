namespace O9K.AIO.Heroes.Dazzle.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Managers.Entity;

    using TargetManager;

    internal class ShadowWave : NukeAbility
    {
        private readonly Core.Entities.Abilities.Heroes.Dazzle.ShadowWave shadowWave;

        public ShadowWave(ActiveAbility ability)
            : base(ability)
        {
            this.shadowWave = (Core.Entities.Abilities.Heroes.Dazzle.ShadowWave)ability;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var input = this.Ability.GetPredictionInput(targetManager.Target);
            var output = this.Ability.GetPredictionOutput(input);

            var ally = EntityManager9.Units.FirstOrDefault(
                x => x.IsUnit && x.IsAlive && !x.IsInvulnerable && x.IsAlly(this.Ability.Owner)
                     && x.Distance(output.TargetPosition) < this.shadowWave.DamageRadius);

            if (ally == null)
            {
                return false;
            }

            if (!this.Ability.UseAbility(ally))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(ally);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}