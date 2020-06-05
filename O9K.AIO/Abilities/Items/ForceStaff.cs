namespace O9K.AIO.Abilities.Items
{
    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using SharpDX;

    using TargetManager;

    internal class ForceStaff : BlinkAbility
    {
        public ForceStaff(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility(this.Owner))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay();
            comboSleeper.Sleep(this.Ability.GetHitTime(this.Owner.InFront(this.Ability.Range)));
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
        {
            if (this.Owner.GetAngle(toPosition) > 0.5f)
            {
                return false;
            }

            if (!this.Ability.UseAbility(this.Owner))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay();
            comboSleeper.Sleep(this.Ability.GetHitTime(this.Owner.InFront(this.Ability.Range)));
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, float minDistance, float blinkDistance)
        {
            var target = targetManager.Target;

            if (this.Owner.Distance(target) < minDistance)
            {
                return false;
            }

            var distance = target.Distance(this.Owner);
            if (distance > this.Ability.Range + blinkDistance)
            {
                return false;
            }

            if (this.Owner.GetAngle(target.Position) > 0.5f)
            {
                return this.Owner.BaseUnit.Move(target.Position);
            }

            if (!this.Ability.UseAbility(this.Owner))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay();
            comboSleeper.Sleep(this.Ability.GetHitTime(target.Position));
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }

        public virtual bool UseAbilityOnTarget(TargetManager targetManager, Sleeper comboSleeper)
        {
            var target = targetManager.Target;
            if (target.GetAngle(this.Owner.Position) > 0.3f)
            {
                return false;
            }

            if (target.Distance(this.Owner) > this.Ability.Range + 100)
            {
                return false;
            }

            if (!this.Ability.UseAbility(target))
            {
                return false;
            }

            comboSleeper.Sleep(this.Ability.GetHitTime(target.Position));
            this.Sleeper.Sleep(this.Ability.GetCastDelay() + 0.5f);
            return true;
        }
    }
}