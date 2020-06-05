namespace O9K.AIO.Heroes.Lina.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_lina))]
    internal class Lina : ControllableUnit
    {
        private readonly Sleeper preventAttackSleeper = new Sleeper();

        private DisableAbility array;

        private DisableAbility atos;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private NukeAbility dragon;

        private EtherealBlade ethereal;

        private EulsScepterOfDivinity euls;

        private ForceStaff force;

        private DisableAbility hex;

        private NukeAbility laguna;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private HurricanePike pike;

        private DebuffAbility veil;

        public Lina(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.lina_dragon_slave, x => this.dragon = new NukeAbility(x) },
                { AbilityId.lina_light_strike_array, x => this.array = new DisableAbility(x) },
                { AbilityId.lina_laguna_blade, x => this.laguna = new NukeAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_hurricane_pike, x => this.pike = new HurricanePike(x) },
                { AbilityId.item_ethereal_blade, x => this.ethereal = new EtherealBlade(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_cyclone, x => this.euls = new EulsScepterOfDivinity(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.lina_light_strike_array, _ => this.array);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            if (targetManager.TargetSleeper.IsSleeping)
            {
                return false;
            }

            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.blink, 550, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 550, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.pike, 550, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
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

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ethereal))
            {
                return true;
            }

            if (abilityHelper.UseKillStealAbility(this.dragon, false))
            {
                return true;
            }

            if (abilityHelper.UseKillStealAbility(this.laguna))
            {
                this.ComboSleeper.ExtendSleep(0.2f);
                return true;
            }

            if (abilityHelper.UseAbilityIfAny(this.euls, this.array))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.euls, false) && abilityHelper.CanBeCasted(this.array, false))
            {
                if (this.Owner.Speed > targetManager.Target.Speed + 50)
                {
                    this.preventAttackSleeper.Sleep(0.5f);
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.array, false))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.laguna))
            {
                this.ComboSleeper.ExtendSleep(0.2f);
                return true;
            }

            if (abilityHelper.UseAbility(this.dragon, false))
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

        protected override bool Attack(Unit9 target, ComboModeMenu comboMenu)
        {
            if (this.preventAttackSleeper.IsSleeping)
            {
                return false;
            }

            return base.Attack(target, comboMenu);
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseDisables(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.array))
            {
                return true;
            }

            return false;
        }
    }
}