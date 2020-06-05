namespace O9K.AIO.Heroes.Phoenix.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    using BaseSunRay = Core.Entities.Abilities.Heroes.Phoenix.SunRay;

    internal class SunRay : NukeAbility
    {
        private readonly BaseSunRay ray;

        public SunRay(ActiveAbility ability)
            : base(ability)
        {
            this.ray = (BaseSunRay)ability;
        }

        public bool IsActive
        {
            get
            {
                return this.ray.IsSunRayActive;
            }
        }

        public bool AutoControl(TargetManager targetManager, Sleeper comboSleeper, float distanceMultiplier)
        {
            var target = targetManager.Target;

            if (this.ray.IsSunRayActive)
            {
                if (this.Owner.GetAngle(target.Position) > 2)
                {
                    if (this.ray.Stop())
                    {
                        this.ray.Stop();
                        comboSleeper.Sleep(0.1f);
                        return true;
                    }

                    return false;
                }

                if (this.Owner.Distance(target) > this.ray.CastRange * distanceMultiplier)
                {
                    if (!this.ray.IsSunRayMoving)
                    {
                        if (this.ray.ToggleMovement())
                        {
                            comboSleeper.Sleep(0.1f);
                            return true;
                        }

                        return false;
                    }
                }
                else
                {
                    if (this.ray.IsSunRayMoving)
                    {
                        if (this.ray.ToggleMovement())
                        {
                            comboSleeper.Sleep(0.1f);
                            return true;
                        }

                        return false;
                    }
                }

                if (this.Owner.BaseUnit.Move(target.Position))
                {
                    comboSleeper.Sleep(0.1f);
                    return true;
                }

                return false;
            }

            if (this.ray.CanBeCasted() && this.ray.CanHit(target))
            {
                if (this.Owner.Distance(target) < 300 && !target.IsStunned && !target.IsHexed && !target.IsRooted)
                {
                    return false;
                }

                if (this.ray.UseAbility(target, HitChance.Low))
                {
                    comboSleeper.Sleep(0.3f);
                    return true;
                }
            }

            return false;
        }
    }
}