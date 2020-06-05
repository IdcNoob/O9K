namespace O9K.AIO.Heroes.SandKing.Abilities
{
    using System.Collections.Generic;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    internal class Epicenter : NukeAbility
    {
        public Epicenter(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            return !targetManager.Target.IsMagicImmune || this.Ability.PiercesMagicImmunity(targetManager.Target);
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (!this.Owner.BaseUnit.IsVisibleToEnemies)
            {
                return true;
            }

            var target = targetManager.Target;
            if (target.IsStunned || target.IsHexed)
            {
                return true;
            }

            return false;
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var blink = usableAbilities.Find(x => x.Ability.Id == AbilityId.item_blink);
            var burrow = usableAbilities.Find(x => x.Ability.Id == AbilityId.sandking_burrowstrike);
            var distance = (burrow?.Ability.CastRange ?? 0) + (blink?.Ability.CastRange ?? 0) + this.Ability.Radius;

            if (this.Owner.Distance(targetManager.Target) < distance)
            {
                return true;
            }

            return false;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility())
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay + 0.5f);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }
    }
}