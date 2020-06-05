namespace O9K.AIO.Heroes.DarkWillow.Abilities
{
    using System.Collections.Generic;

    using AIO.Abilities;
    using AIO.Abilities.Items;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    internal class EulsScepterOfDivinityDarkWillow : EulsScepterOfDivinity
    {
        public EulsScepterOfDivinityDarkWillow(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var crown = usableAbilities.Find(x => x.Ability.Id == AbilityId.dark_willow_cursed_crown);
            if (crown != null)
            {
                return true;
            }

            var crownDebuff = targetManager.Target.GetModifier("modifier_dark_willow_cursed_crown");
            if (crownDebuff == null || crownDebuff.RemainingTime < this.Ability.Duration + 0.1f)
            {
                return false;
            }

            return true;
        }
    }
}