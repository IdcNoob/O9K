namespace O9K.AIO.Heroes.Ursa.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_ursa))]
    internal class Ursa : ControllableUnit
    {
        private DisableAbility abyssal;

        private ShieldAbility bkb;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private DebuffAbility diffusal;

        private ShieldAbility enrage;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private BuffAbility overpower;

        private SpeedBuffAbility phase;

        private NukeAbility shock;

        public Ursa(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.ursa_earthshock, x => this.shock = new NukeAbility(x) },
                { AbilityId.ursa_overpower, x => this.overpower = new BuffAbility(x) },
                { AbilityId.ursa_enrage, x => this.enrage = new Enrage(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_diffusal_blade, x => this.diffusal = new DebuffAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.bkb, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.abyssal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.diffusal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.overpower))
            {
                return true;
            }

            if (this.Owner.Distance(targetManager.Target) > this.Owner.GetAttackRange(targetManager.Target)
                && abilityHelper.UseAbility(this.shock))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 400, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.enrage, 300))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }
    }
}