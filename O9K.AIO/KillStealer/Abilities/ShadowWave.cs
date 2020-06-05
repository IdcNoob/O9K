namespace O9K.AIO.KillStealer.Abilities
{
    using System.Linq;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    [AbilityId(AbilityId.dazzle_shadow_wave)]
    internal class ShadowWave : KillStealAbility
    {
        private readonly Core.Entities.Abilities.Heroes.Dazzle.ShadowWave shadowWave;

        private Unit9 waveTarget;

        public ShadowWave(ActiveAbility ability)
            : base(ability)
        {
            this.shadowWave = (Core.Entities.Abilities.Heroes.Dazzle.ShadowWave)ability;
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (!base.ShouldCast(target))
            {
                return false;
            }

            var input = this.Ability.GetPredictionInput(target);
            var output = this.Ability.GetPredictionOutput(input);

            var ally = EntityManager9.Units.FirstOrDefault(
                x => x.IsUnit && x.IsAlive && !x.IsInvulnerable && x.IsAlly(this.Ability.Owner)
                     && x.Distance(output.TargetPosition) < this.shadowWave.DamageRadius);

            if (ally == null)
            {
                return false;
            }

            this.waveTarget = ally;
            return true;
        }

        public override bool UseAbility(Unit9 target)
        {
            if (this.waveTarget?.IsValid != true)
            {
                return false;
            }

            return base.UseAbility(this.waveTarget);
        }
    }
}