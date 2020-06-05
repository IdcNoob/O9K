namespace O9K.AIO.Heroes.TemplarAssassin.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    [UnitName("npc_dota_templar_assassin_psionic_trap")]
    internal class PsionicTrap : ControllableUnit
    {
        private TrapExplode trapExplode;

        public PsionicTrap(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.templar_assassin_self_trap, x => this.trapExplode = new TrapExplode(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            if (comboModeMenu.IsHarassCombo)
            {
                return false;
            }

            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.trapExplode))
            {
                return true;
            }

            return false;
        }
    }
}