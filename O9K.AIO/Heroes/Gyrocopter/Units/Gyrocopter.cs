namespace O9K.AIO.Heroes.Gyrocopter.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_gyrocopter))]
    internal class Gyrocopter : ControllableUnit
    {
        private NukeAbility barrage;

        private NukeAbility callDown;

        private FlakCannon flak;

        private ForceStaff force;

        private BuffAbility manta;

        private NukeAbility missile;

        private ShieldAbility mjollnir;

        private SpeedBuffAbility phase;

        private HurricanePike pike;

        public Gyrocopter(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.gyrocopter_rocket_barrage, x => this.barrage = new NukeAbility(x) },
                { AbilityId.gyrocopter_homing_missile, x => this.missile = new NukeAbility(x) },
                { AbilityId.gyrocopter_flak_cannon, x => this.flak = new FlakCannon(x) },
                { AbilityId.gyrocopter_call_down, x => this.callDown = new NukeAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_hurricane_pike, x => this.pike = new HurricanePike(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_manta, x => this.manta = new BuffAbility(x) },
                { AbilityId.item_mjollnir, x => this.mjollnir = new ShieldAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.gyrocopter_homing_missile, _ => this.missile);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.missile))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.callDown))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 500, 300))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.pike, 500, 300))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.flak))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.barrage))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.manta, this.Owner.GetAttackRange()))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.mjollnir, 600))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.pike) && !this.MoveSleeper.IsSleeping)
            {
                if (this.pike.UseAbilityOnTarget(targetManager, this.ComboSleeper))
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

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (target != null && this.Owner.HasModifier("modifier_gyrocopter_rocket_barrage")
                               && this.Owner.Distance(target) > this.barrage.Ability.Radius / 2)
            {
                return this.Move(target.Position);
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseDisables(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.missile))
            {
                return true;
            }

            return false;
        }
    }
}