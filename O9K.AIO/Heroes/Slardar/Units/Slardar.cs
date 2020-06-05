namespace O9K.AIO.Heroes.Slardar.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_slardar))]
    internal class Slardar : ControllableUnit
    {
        private DebuffAbility amplify;

        private BuffAbility armlet;

        private ShieldAbility bkb;

        private ShieldAbility bladeMail;

        private BlinkDaggerAOE blink;

        private DisableAbility bloodthorn;

        private DisableAbility crush;

        private ForceStaff force;

        private DisableAbility halberd;

        private DebuffAbility medallion;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private DebuffAbility solar;

        private BuffAbility sprint;

        public Slardar(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.slardar_sprint, x => this.sprint = new BuffAbility(x) },
                { AbilityId.slardar_slithereen_crush, x => this.crush = new DisableAbility(x) },
                { AbilityId.slardar_amplify_damage, x => this.amplify = new DebuffAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkDaggerAOE(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_armlet, x => this.armlet = new BuffAbility(x) },
                { AbilityId.item_solar_crest, x => this.solar = new DebuffAbility(x) },
                { AbilityId.item_medallion_of_courage, x => this.medallion = new DebuffAbility(x) },
                { AbilityId.item_heavens_halberd, x => this.halberd = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.slardar_slithereen_crush, _ => this.crush);
            this.MoveComboAbilities.Add(AbilityId.slardar_sprint, _ => this.sprint);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink))
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

            if (abilityHelper.CanBeCastedIfCondition(this.blink, this.crush))
            {
                if (abilityHelper.UseAbility(this.bkb))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.crush))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.crush))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 500, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.sprint))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.solar))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.halberd))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.medallion))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bladeMail, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.armlet, 400))
            {
                return true;
            }

            if (!abilityHelper.CanBeCasted(this.crush) && abilityHelper.UseAbility(this.amplify))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }

        protected override bool MoveComboUseBuffs(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBuffs(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.sprint))
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

            if (abilityHelper.UseAbility(this.crush))
            {
                return true;
            }

            return false;
        }
    }
}