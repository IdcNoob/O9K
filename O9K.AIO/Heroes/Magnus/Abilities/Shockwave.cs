namespace O9K.AIO.Heroes.Magnus.Abilities
{
    using System.Collections.Generic;

    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using TargetManager;

    internal class Shockwave : NukeAbility
    {
        public Shockwave(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            if (this.Owner.Distance(targetManager.Target) < 500)
            {
                return true;
            }

            var skewer = usableAbilities.Find(x => x.Ability.Id == AbilityId.magnataur_skewer);
            if (skewer != null)
            {
                var polarity = usableAbilities.Find(x => x.Ability.Id == AbilityId.magnataur_reverse_polarity);
                if (polarity != null)
                {
                    return false;
                }
            }

            var blink = usableAbilities.Find(x => x.Ability.Id == AbilityId.item_blink || x.Ability.Id == AbilityId.item_force_staff);
            if (blink == null || this.Ability.GetDamage(targetManager.Target) > targetManager.Target.Health)
            {
                return true;
            }

            if (this.Owner.Distance(targetManager.Target) > 800)
            {
                return false;
            }

            return true;
        }
    }
}