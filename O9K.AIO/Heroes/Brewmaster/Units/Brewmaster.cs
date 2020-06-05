namespace O9K.AIO.Heroes.Brewmaster.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;
    using AIO.Abilities.Items;
    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_brewmaster))]
    internal class Brewmaster : ControllableUnit
    {
        private DisableAbility abyssal;

        private BlinkDaggerAOE blink;

        private BuffAbility brawler;

        private DebuffAbility cinder;

        private NukeAbility clap;

        private SpeedBuffAbility phase;

        private PrimalSplit split;

        public Brewmaster(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.brewmaster_thunder_clap, x => this.clap = new NukeAbility(x) },
                { AbilityId.brewmaster_cinder_brew, x => this.cinder = new DebuffAbility(x) },
                { AbilityId.brewmaster_primal_split, x => this.split = new PrimalSplit(x) },
                { AbilityId.brewmaster_drunken_brawler, x => this.brawler = new BuffAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkDaggerAOE(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.brewmaster_thunder_clap, _ => this.clap);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.abyssal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.cinder))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.clap))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.clap))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 400, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.split))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.brawler, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
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

            if (abilityHelper.UseAbility(this.clap))
            {
                return true;
            }

            return false;
        }
    }
}