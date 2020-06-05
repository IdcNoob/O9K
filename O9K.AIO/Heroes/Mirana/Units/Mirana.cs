namespace O9K.AIO.Heroes.Mirana.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_mirana))]
    internal class Mirana : ControllableUnit
    {
        private DisableAbility arrow;

        private DisableAbility atos;

        private BlinkAbility blink;

        private DebuffAbility diffusal;

        private ForceStaff leap;

        private BuffAbility manta;

        private SpeedBuffAbility phase;

        private NukeAbility starstorm;

        public Mirana(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.mirana_starfall, x => this.starstorm = new NukeAbility(x) },
                { AbilityId.mirana_arrow, x => this.arrow = new DisableAbility(x) },
                { AbilityId.mirana_leap, x => this.leap = new ForceStaff(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_diffusal_blade, x => this.diffusal = new DebuffAbility(x) },
                { AbilityId.item_manta, x => this.manta = new BuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.mirana_leap, _ => this.leap);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.starstorm))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.diffusal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, this.Owner.GetAttackRange(), 350))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.arrow))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.leap, this.Owner.GetAttackRange(targetManager.Target) + 100, 800))
            {
                return true;
            }

            if (abilityHelper.UseForceStaffAway(this.leap, 200))
            {
                this.OrbwalkSleeper.Sleep(0.2f);
                this.ComboSleeper.Sleep(0.2f);
                this.leap.Sleeper.Sleep(2f);
                return true;
            }

            if (abilityHelper.UseAbility(this.manta, this.Owner.GetAttackRange()))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }

        protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBlinks(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.leap))
            {
                return true;
            }

            return false;
        }
    }
}