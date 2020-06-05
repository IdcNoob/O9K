namespace O9K.AIO.Heroes.Axe.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    internal class MeteorHammerAxe : DisableAbility
    {
        public MeteorHammerAxe(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var input = this.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            input.Delay -= ((IChanneled)this.Ability).ChannelTime;
            var output = this.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            if (!this.Ability.UseAbility(output.CastPosition))
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(output.CastPosition) + 0.5f;
            var delay = this.Ability.GetCastDelay(output.CastPosition);

            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }

        protected override bool ChainStun(Unit9 target, bool invulnerability)
        {
            return true;
        }
    }
}