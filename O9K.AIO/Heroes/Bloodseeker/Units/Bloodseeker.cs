namespace O9K.AIO.Heroes.Bloodseeker.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_bloodseeker))]
    internal class Bloodseeker : ControllableUnit
    {
        private DisableAbility abyssal;

        private ShieldAbility bladeMail;

        private BloodRite blood;

        private EulsScepterOfDivinity euls;

        private ShieldAbility mjollnir;

        private SpeedBuffAbility phase;

        private BuffAbility rage;

        private TargetableAbility rupture;

        public Bloodseeker(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.bloodseeker_bloodrage, x => this.rage = new BuffAbility(x) },
                { AbilityId.bloodseeker_blood_bath, x => this.blood = new BloodRite(x) },
                { AbilityId.bloodseeker_rupture, x => this.rupture = new TargetableAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
                { AbilityId.item_cyclone, x => this.euls = new EulsScepterOfDivinity(x) },
                { AbilityId.item_mjollnir, x => this.mjollnir = new ShieldAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.abyssal))
            {
                return true;
            }

            if (!targetManager.Target.IsRuptured)
            {
                if (abilityHelper.UseAbilityIfAny(this.euls, this.blood))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbilityIfCondition(this.blood, this.euls, this.rupture))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.rupture))
            {
                this.rupture.Sleeper.ExtendSleep(1f);
                this.ComboSleeper.ExtendSleep(0.25f);
                return true;
            }

            if (abilityHelper.UseAbility(this.rage))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bladeMail, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.mjollnir, 600))
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