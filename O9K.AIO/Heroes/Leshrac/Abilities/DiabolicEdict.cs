namespace O9K.AIO.Heroes.Leshrac.Abilities
{
    using System.Collections.Generic;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    internal class DiabolicEdict : UsableAbility
    {
        public DiabolicEdict(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            return false;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if ((!target.IsStunned && !target.IsInvulnerable && !target.IsRooted) || this.Owner.Distance(target) > 400)
            {
                return false;
            }

            if (target.IsEthereal)
            {
                return false;
            }

            return true;
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var target = targetManager.Target;
            var immobileDuration = target.GetImmobilityDuration();
            if (immobileDuration > 0)
            {
                var splitEarth = usableAbilities.Find(x => x.Ability.Id == AbilityId.leshrac_split_earth);
                if (splitEarth == null)
                {
                    return true;
                }

                if (immobileDuration < splitEarth.Ability.GetHitTime(target) + this.Ability.GetHitTime(target))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility())
            {
                return false;
            }

            this.Sleeper.Sleep(this.Ability.GetCastDelay() + 0.5f);
            return true;
        }
    }
}