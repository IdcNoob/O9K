namespace O9K.AIO.Heroes.Lion.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;
    using AIO.Abilities.Items;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_lion))]
    internal class Lion : ControllableUnit
    {
        private BlinkAbility blink;

        private NukeAbility dagon;

        private TargetableAbility drain;

        private EtherealBlade ethereal;

        private NukeAbility finger;

        private ForceStaff force;

        private DisableAbility hex;

        private DisableAbility impale;

        private DebuffAbility veil;

        public Lion(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.lion_impale, x => this.impale = new DisableAbility(x) },
                { AbilityId.lion_mana_drain, x => this.drain = new ManaDrain(x) },
                { AbilityId.lion_voodoo, x => this.hex = new DisableAbility(x) },
                { AbilityId.lion_finger_of_death, x => this.finger = new NukeAbility(x) },

                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_dagon_5, x => this.dagon = new NukeAbility(x) },
                { AbilityId.item_ethereal_blade, x => this.ethereal = new EtherealBlade(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.lion_impale, _ => this.impale);
            this.MoveComboAbilities.Add(AbilityId.lion_voodoo, _ => this.hex);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (!comboModeMenu.IsHarassCombo && this.drain?.Ability.IsChanneling == true)
            {
                if (abilityHelper.CanBeCasted(this.hex, true, true, false) || abilityHelper.CanBeCasted(this.impale, true, true, false))
                {
                    this.Owner.BaseUnit.Stop();
                    this.ComboSleeper.Sleep(0.05f);
                    return true;
                }
            }

            if (abilityHelper.UseKillStealAbility(this.finger))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ethereal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dagon))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.impale))
            {
                return true;
            }

            if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 550, 350))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 550, 350))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfNone(this.finger, this.hex, this.impale))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.drain))
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

            if (abilityHelper.UseMoveAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.impale))
            {
                return true;
            }

            return false;
        }
    }
}