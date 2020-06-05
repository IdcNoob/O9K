namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables.Unique
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    using Specials;

    [AbilityId(AbilityId.item_cyclone)]
    internal class EulsDisable : OldDisableAbility
    {
        public EulsDisable(IDisable ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted(
            Unit9 target,
            List<OldDisableAbility> disables,
            List<OldSpecialAbility> specials,
            ComboModeMenu menu)
        {
            if (!this.CanBeCasted(target, menu))
            {
                return false;
            }

            if (disables.Any(x => !x.Disable.UnitTargetCast && x.Disable.Owner.Equals(this.Ability.Owner) && x.CanBeCasted(target, menu)))
            {
                return true;
            }

            return false;
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (!target.IsInNormalState && !target.IsTeleporting && !target.IsChanneling)
            {
                return false;
            }

            return base.ShouldCast(target);
        }
    }
}