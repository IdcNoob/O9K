namespace O9K.AIO.Heroes.Pudge.Abilities
{
    using System.Collections.Generic;

    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using Ensage;

    using TargetManager;

    internal class BlinkDaggerPudge : BlinkAbility
    {
        public BlinkDaggerPudge(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var dismember = usableAbilities.Find(x => x.Ability.Id == AbilityId.pudge_dismember);
            if (dismember == null)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            return this.UseAbility(targetManager, comboSleeper, 100, 25);
        }
    }
}