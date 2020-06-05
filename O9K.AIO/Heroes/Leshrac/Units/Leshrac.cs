namespace O9K.AIO.Heroes.Leshrac.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_leshrac))]
    internal class Leshrac : ControllableUnit
    {
        private DisableAbility atos;

        private BlinkAbility blink;

        private DiabolicEdict diabolic;

        private EtherealBlade ethereal;

        private EulsScepterOfDivinity euls;

        private DisableAbility hex;

        private NukeAbility lightning;

        private PulseNova nova;

        private DebuffAbility shiva;

        private SplitEarth splitEarth;

        private DebuffAbility veil;

        public Leshrac(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                {
                    AbilityId.leshrac_split_earth, x =>
                        {
                            this.splitEarth = new SplitEarth(x);
                            if (this.lightning != null)
                            {
                                this.splitEarth.Storm = this.lightning;
                            }

                            this.splitEarth.FailSafe = this.FailSafe;
                            return this.splitEarth;
                        }
                },
                { AbilityId.leshrac_diabolic_edict, x => this.diabolic = new DiabolicEdict(x) },
                {
                    AbilityId.leshrac_lightning_storm, x =>
                        {
                            this.lightning = new NukeAbility(x);
                            if (this.splitEarth != null)
                            {
                                this.splitEarth.Storm = this.lightning;
                            }

                            return this.lightning;
                        }
                },
                { AbilityId.leshrac_pulse_nova, x => this.nova = new PulseNova(x) },

                { AbilityId.item_ethereal_blade, x => this.ethereal = new EtherealBlade(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_cyclone, x => this.euls = new EulsScepterOfDivinity(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.leshrac_split_earth, _ => this.splitEarth);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.blink, 550, 350))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ethereal))
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

            if (abilityHelper.UseAbilityIfAny(this.euls, this.splitEarth))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.nova, false, false))
            {
                if (this.nova.AutoToggle(targetManager))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbilityIfCondition(this.diabolic, this.splitEarth))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.lightning))
            {
                this.euls?.Sleeper.Sleep(0.5f);
                return true;
            }

            if (abilityHelper.UseAbilityIfNone(this.splitEarth, this.lightning, this.euls, this.atos))
            {
                return true;
            }

            return false;
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (target?.IsMagicImmune != true
                && ((comboMenu?.IsAbilityEnabled(this.lightning.Ability) == true && this.lightning.Ability.CanBeCasted())
                    || (comboMenu?.IsAbilityEnabled(this.splitEarth.Ability) == true && this.splitEarth.Ability.CanBeCasted())))
            {
                attack = false;
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseDisables(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.splitEarth))
            {
                return true;
            }

            return false;
        }
    }
}