namespace O9K.AIO.Heroes.Alchemist.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.Alchemist;
    using Core.Entities.Units;
    using Core.Helpers;

    using TargetManager;

    internal class UnstableConcoction : DisableAbility
    {
        private readonly UnstableConcoctionThrow unstableConcoction;

        public UnstableConcoction(ActiveAbility ability)
            : base(ability)
        {
            this.unstableConcoction = (UnstableConcoctionThrow)ability;
        }

        public bool ThrowAway(TargetManager targetManager, Sleeper comboSleeper)
        {
            var target = targetManager.Target;
            if (target.IsVisible && !target.IsMagicImmune && !target.IsInvulnerable)
            {
                return false;
            }

            var modifier = this.Owner.GetModifier("modifier_alchemist_unstable_concoction");
            if (modifier == null)
            {
                return false;
            }

            if (modifier.ElapsedTime > this.unstableConcoction.BrewTime - this.Ability.CastPoint)
            {
                var otherTarget = targetManager.EnemyHeroes.Find(x => this.Ability.CanHit(x));
                if (otherTarget == null)
                {
                    return false;
                }

                if (!this.Ability.UseAbility(otherTarget))
                {
                    return false;
                }

                var delay = this.Ability.GetCastDelay(otherTarget);
                comboSleeper.Sleep(delay);
                this.Sleeper.Sleep(this.Ability.GetHitTime(otherTarget) + 0.5f);
                this.OrbwalkSleeper.Sleep(delay);
                return true;
            }

            return false;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.IsUsable)
            {
                if (this.Ability.UseAbility())
                {
                    comboSleeper.Sleep(0.1f);
                    return true;
                }
            }

            var modifier = this.Owner.GetModifier("modifier_alchemist_unstable_concoction");
            if (modifier == null)
            {
                return false;
            }

            if (modifier.ElapsedTime < this.unstableConcoction.BrewTime - this.Ability.CastPoint)
            {
                var target = targetManager.Target;
                if (this.Owner.Distance(target) < this.Ability.CastRange - 100 || this.Owner.Speed > target.Speed
                                                                               || target.GetImmobilityDuration() > 0)
                {
                    return false;
                }
            }

            if (!this.Ability.UseAbility(targetManager.Target))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(this.Ability.GetHitTime(targetManager.Target) + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        protected override bool ChainStun(Unit9 target, bool invulnerability)
        {
            return true;
        }
    }
}