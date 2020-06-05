namespace O9K.AIO.Heroes.Spectre.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_spectre))]
    internal class Spectre : ControllableUnit
    {
        private DisableAbility abyssal;

        private ShieldAbility bkb;

        private DisableAbility bloodthorn;

        private NukeAbility dagger;

        private DaggerMove daggerMove;

        private DebuffAbility diffusal;

        private Haunt haunt;

        private DisableAbility hex;

        private BuffAbility manta;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private Reality reality;

        private TargetableAbility shadowStep;

        private DebuffAbility urn;

        private DebuffAbility vessel;

        public Spectre(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.spectre_spectral_dagger, x => this.dagger = new NukeAbility(x) },
                { AbilityId.spectre_reality, x => this.reality = new Reality(x) },
                { AbilityId.spectre_haunt, x => this.haunt = new Haunt(x) },
                { (AbilityId)7851, x => this.shadowStep = new TargetableAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_diffusal_blade, x => this.diffusal = new DebuffAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
                { AbilityId.item_manta, x => this.manta = new BuffAbility(x) },
                { AbilityId.item_spirit_vessel, x => this.vessel = new DebuffAbility(x) },
                { AbilityId.item_urn_of_shadows, x => this.urn = new DebuffAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.spectre_spectral_dagger, x => this.daggerMove = new DaggerMove(x));
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.abyssal))
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

            if (abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.diffusal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shadowStep))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.haunt))
            {
                return true;
            }

            if (this.haunt?.Ability.TimeSinceCasted < 10 || this.shadowStep?.Ability.TimeSinceCasted < 10)
            {
                if (abilityHelper.UseAbility(this.reality))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.bkb))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dagger))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.manta, this.Owner.GetAttackRange()))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.vessel))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.urn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }

        public override void EndCombo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            base.EndCombo(targetManager, comboModeMenu);
            if (this.reality != null)
            {
                this.reality.RealityUseOnFakeTarget = true;
            }
        }

        protected override bool MoveComboUseBuffs(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBuffs(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.daggerMove))
            {
                return true;
            }

            return false;
        }
    }
}