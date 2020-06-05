namespace O9K.AIO.Heroes.Timbersaw.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.Timbersaw;
    using Core.Helpers;

    using SharpDX;

    using TargetManager;

    using BaseChakram = Core.Entities.Abilities.Heroes.Timbersaw.Chakram;

    internal class Chakram : NukeAbility
    {
        private readonly BaseChakram chakram;

        private Vector3 castPosition;

        public Chakram(ActiveAbility ability)
            : base(ability)
        {
            this.chakram = (BaseChakram)ability;
        }

        public ReturnChakram ReturnChakram
        {
            get
            {
                return this.chakram.ReturnChakram;
            }
        }

        public bool IsDamaging(TargetManager targetManager)
        {
            if (this.Sleeper.IsSleeping)
            {
                return false;
            }

            if (targetManager.Target.Distance(this.castPosition) > this.Ability.Radius)
            {
                return false;
            }

            return true;
        }

        public bool Return()
        {
            return this.chakram.UseAbility();
        }

        public bool ShouldReturnChakram(TargetManager targetManager, int damagingChakrams)
        {
            if (this.Sleeper.IsSleeping || !this.chakram.ReturnChakram.CanBeCasted())
            {
                return false;
            }

            var target = targetManager.Target;

            if (this.IsDamaging(targetManager))
            {
                if (target.Health < (this.Ability.GetDamage(target) / 2) * damagingChakrams)
                {
                    return true;
                }

                if (target.GetAngle(this.castPosition) < 0.75f)
                {
                    return false;
                }

                if (target.IsMoving && target.Distance(this.castPosition) > this.Ability.Radius - 50)
                {
                    return true;
                }
            }
            else
            {
                if (target.GetAngle(this.castPosition) > 0.5f)
                {
                    return true;
                }
            }

            return false;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var input = this.Ability.GetPredictionInput(target, targetManager.EnemyHeroes);

            if (target.GetAngle(this.Owner.Position) > 2)
            {
                input.Delay += 0.5f;
            }

            var output = this.Ability.GetPredictionOutput(input);
            this.castPosition = output.CastPosition;

            if (!this.Ability.UseAbility(this.castPosition))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(this.castPosition);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(this.Ability.GetHitTime(this.castPosition));
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}