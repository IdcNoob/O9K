namespace O9K.AIO.Heroes.Windranger.Abilities
{
    using System.Collections.Generic;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    internal class FocusFire : TargetableAbility
    {
        public FocusFire(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            var target = targetManager.Target;

            if (target.IsEthereal)
            {
                return false;
            }

            if (target.Distance(this.Owner) < 300 && target.HealthPercentage < 50)
            {
                return true;
            }

            if (!target.IsStunned && !target.IsRooted && !target.IsHexed)
            {
                return false;
            }

            return true;
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var target = targetManager.Target;
            var powershot = usableAbilities.Find(x => x.Ability.Id == AbilityId.windrunner_powershot);
            if (powershot == null)
            {
                return true;
            }

            if (powershot.Ability.GetDamage(target) > target.Health)
            {
                return false;
            }

            return true;
        }
    }
}