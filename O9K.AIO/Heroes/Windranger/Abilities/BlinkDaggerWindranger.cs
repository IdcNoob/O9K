namespace O9K.AIO.Heroes.Windranger.Abilities
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class BlinkDaggerWindranger : BlinkAbility
    {
        private Vector3 blinkPosition;

        public BlinkDaggerWindranger(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            if (!(usableAbilities.FirstOrDefault(x => x.Ability.Id == AbilityId.windrunner_shackleshot) is Shackleshot shackle))
            {
                return false;
            }

            this.blinkPosition = shackle.GetBlinkPosition(targetManager, this.Ability.CastRange);
            return !this.blinkPosition.IsZero;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(this.blinkPosition))
            {
                return false;
            }

            this.Sleeper.Sleep(this.Ability.GetCastDelay(targetManager.Target));
            return true;
        }
    }
}