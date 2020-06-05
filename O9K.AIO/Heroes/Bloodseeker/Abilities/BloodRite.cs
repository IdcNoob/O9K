namespace O9K.AIO.Heroes.Bloodseeker.Abilities
{
    using System.Collections.Generic;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    internal class BloodRite : DisableAbility
    {
        public BloodRite(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            if (targetManager.Target.IsRuptured)
            {
                return true;
            }

            var rupture = usableAbilities.Find(x => x.Ability.Id == AbilityId.bloodseeker_rupture);
            if (rupture != null && targetManager.Target.GetImmobilityDuration() <= 0)
            {
                return false;
            }

            var euls = usableAbilities.Find(x => x.Ability.Id == AbilityId.item_cyclone);
            if (euls != null)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;

            if (target.IsRuptured)
            {
                if (target.IsMoving)
                {
                    return false;
                }

                if (!this.Ability.UseAbility(target, HitChance.Low))
                {
                    return false;
                }
            }

            if (!this.Ability.UseAbility(target, targetManager.EnemyHeroes, HitChance.Low))
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(target) + 0.5f;
            var delay = this.Ability.GetCastDelay(target);

            target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }
    }
}