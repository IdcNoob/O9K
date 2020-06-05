namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables.Unique
{
    using System.Collections.Generic;

    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Specials;

    [AbilityId(AbilityId.visage_summon_familiars_stone_form)]
    internal class StoneFormDisable : OldDisableAbility
    {
        private static readonly Sleeper Sleeper = new Sleeper();

        public StoneFormDisable(IDisable ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted(
            Unit9 target,
            List<OldDisableAbility> disables,
            List<OldSpecialAbility> specials,
            ComboModeMenu menu)
        {
            if (Sleeper.IsSleeping)
            {
                return false;
            }

            return base.CanBeCasted(target, disables, specials, menu);
        }

        public override bool Use(Unit9 target)
        {
            if (base.Use(target))
            {
                Sleeper.Sleep(1f);
            }

            return false;
        }
    }
}