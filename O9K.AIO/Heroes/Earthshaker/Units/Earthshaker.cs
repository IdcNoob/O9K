namespace O9K.AIO.Heroes.Earthshaker.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;
    using AIO.Abilities.Items;
    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Modes;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_earthshaker))]
    internal class Earthshaker : ControllableUnit
    {
        private BlinkDaggerShaker blink;

        private EchoSlam echo;

        private Fissure fissure;

        private ForceStaff force;

        private EnchantTotem totem;

        public Earthshaker(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.earthshaker_fissure, x => this.fissure = new Fissure(x) },
                { AbilityId.earthshaker_enchant_totem, x => this.totem = new EnchantTotem(x) },
                { AbilityId.earthshaker_echo_slam, x => this.echo = new EchoSlam(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkDaggerShaker(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            switch (comboModeMenu)
            {
                case EarthshakerComboModeMenu shakerMenu when shakerMenu.PreferEnchantTotem:
                    return this.TotemCombo(targetManager, abilityHelper);
                case EarthshakerEchoSlamComboModeMenu _:
                    return this.EchoSlamCombo(targetManager, abilityHelper);
            }

            if (abilityHelper.UseAbility(this.echo))
            {
                return true;
            }

            if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.echo))
            {
                UpdateManager.BeginInvoke(
                    () =>
                        {
                            this.echo.ForceUseAbility(targetManager, this.ComboSleeper);
                            this.OrbwalkSleeper.ExtendSleep(0.1f);
                            this.ComboSleeper.ExtendSleep(0.1f);
                        },
                    111);
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.totem))
            {
                UpdateManager.BeginInvoke(
                    () =>
                        {
                            this.totem.ForceUseAbility(targetManager, this.ComboSleeper);
                            this.OrbwalkSleeper.ExtendSleep(0.2f);
                            this.ComboSleeper.ExtendSleep(0.2f);
                        },
                    111);
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 500, 100))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.fissure))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.totem))
            {
                return true;
            }

            return false;
        }

        public bool TotemCombo(TargetManager targetManager, AbilityHelper abilityHelper)
        {
            var distance = this.Owner.Distance(targetManager.Target);

            if (distance < 250 && this.Owner.HasModifier("modifier_earthshaker_enchant_totem"))
            {
                return false;
            }

            if (abilityHelper.UseAbility(this.totem))
            {
                return true;
            }

            if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.totem))
            {
                if (!this.Owner.HasModifier("modifier_earthshaker_enchant_totem"))
                {
                    UpdateManager.BeginInvoke(
                        () =>
                            {
                                this.totem.ForceUseAbility(targetManager, this.ComboSleeper);
                                this.OrbwalkSleeper.ExtendSleep(0.2f);
                                this.ComboSleeper.ExtendSleep(0.2f);
                            },
                        111);
                }
                else if (this.Owner.BaseUnit.Attack(targetManager.Target.BaseUnit))
                {
                    this.OrbwalkSleeper.ExtendSleep(0.1f);
                    this.ComboSleeper.ExtendSleep(0.1f);
                    return true;
                }

                return true;
            }

            if (abilityHelper.UseAbility(this.force, 500, 100))
            {
                return true;
            }

            if (!abilityHelper.CanBeCasted(this.totem) && (!abilityHelper.CanBeCasted(this.blink) || distance < 300))
            {
                if (abilityHelper.UseAbility(this.fissure))
                {
                    this.OrbwalkSleeper.ExtendSleep(0.1f);
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.echo))
            {
                return true;
            }

            return false;
        }

        private bool EchoSlamCombo(TargetManager targetManager, AbilityHelper abilityHelper)
        {
            if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.echo))
            {
                this.ComboSleeper.ExtendSleep(0.1f);
                this.OrbwalkSleeper.ExtendSleep(0.1f);
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.echo))
            {
                UpdateManager.BeginInvoke(
                    () =>
                        {
                            this.echo.ForceUseAbility(targetManager, this.ComboSleeper);
                            this.ComboSleeper.ExtendSleep(0.2f);
                            this.OrbwalkSleeper.ExtendSleep(0.2f);
                        },
                    111);
                return true;
            }

            if (abilityHelper.CanBeCasted(this.echo, false, false))
            {
                return false;
            }

            if (abilityHelper.UseAbility(this.totem))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.fissure))
            {
                return true;
            }

            return false;
        }
    }
}