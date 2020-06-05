namespace O9K.AIO.Heroes.Necrophos.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;
    using Abilities.Items;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_necrolyte))]
    internal class Necrophos : ControllableUnit
    {
        private DisableAbility atos;

        private ShieldAbility bladeMail;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private NukeAbility dagon;

        private EtherealBlade ethereal;

        private DisableAbility hex;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private NukeAbility pulse;

        private NukeAbility scythe;

        private DebuffAbility shiva;

        private ShieldAbility shroud;

        private DebuffAbility veil;

        public Necrophos(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.necrolyte_death_pulse, x => this.pulse = new NukeAbility(x) },
                { AbilityId.necrolyte_sadist, x => this.shroud = new ShieldAbility(x) },
                { AbilityId.necrolyte_reapers_scythe, x => this.scythe = new NukeAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_ethereal_blade, x => this.ethereal = new EtherealBlade(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_dagon_5, x => this.dagon = new NukeAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.blink, 700, 300))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
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

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.pulse))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.scythe))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shroud, 300))
            {
                this.ComboSleeper.ExtendSleep(0.3f);
                return true;
            }

            if (!this.Owner.IsAttackImmune && !abilityHelper.CanBeCasted(this.shroud))
            {
                if (abilityHelper.UseAbility(this.bladeMail, 500))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }
    }
}