namespace O9K.AIO.Heroes.Brewmaster.Units
{
    using System;
    using System.Collections.Generic;

    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using TargetManager;

    [UnitName("npc_dota_brewmaster_earth_1")]
    [UnitName("npc_dota_brewmaster_earth_2")]
    [UnitName("npc_dota_brewmaster_earth_3")]
    internal class Earth : ControllableUnit
    {
        private DisableAbility boulder;

        private NukeAbility clap;

        public Earth(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.brewmaster_earth_hurl_boulder, x => this.boulder = new DisableAbility(x) },
                { AbilityId.brewmaster_thunder_clap, x => this.clap = new NukeAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.brewmaster_earth_hurl_boulder, _ => this.boulder);
            this.MoveComboAbilities.Add(AbilityId.brewmaster_thunder_clap, _ => this.clap);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.boulder))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.clap))
            {
                return true;
            }

            return false;
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseDisables(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.boulder))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.clap))
            {
                return true;
            }

            return false;
        }
    }
}