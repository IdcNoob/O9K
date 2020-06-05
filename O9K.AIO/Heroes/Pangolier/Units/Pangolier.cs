namespace O9K.AIO.Heroes.Pangolier.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_pangolier))]
    internal class Pangolier : ControllableUnit, IDisposable
    {
        private readonly Sleeper ultSleeper = new Sleeper();

        private DisableAbility abyssal;

        private BlinkDaggerPangolier blink;

        private ShieldCrash crash;

        private DebuffAbility diffusal;

        private ShieldAbility mjollnir;

        private SwashbuckleBlink moveSwashbuckle;

        private Swashbuckle swashbuckle;

        private RollingThunder thunder;

        public Pangolier(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.pangolier_swashbuckle, x => this.swashbuckle = new Swashbuckle(x) },
                { AbilityId.pangolier_shield_crash, x => this.crash = new ShieldCrash(x) },
                { AbilityId.pangolier_gyroshell, x => this.thunder = new RollingThunder(x) },

                { AbilityId.item_diffusal_blade, x => this.diffusal = new DebuffAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
                { AbilityId.item_mjollnir, x => this.mjollnir = new ShieldAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkDaggerPangolier(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.pangolier_swashbuckle, x => this.moveSwashbuckle = new SwashbuckleBlink(x));

            Player.OnExecuteOrder += this.OnExecuteOrder;
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            if (this.ultSleeper)
            {
                return false;
            }

            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.abyssal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.diffusal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.swashbuckle))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.mjollnir, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.crash))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.thunder, this.blink))
            {
                this.ComboSleeper.Sleep(this.thunder.Ability.CastPoint + 0.1f);
                return true;
            }

            if (abilityHelper.UseAbility(this.blink))
            {
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            Player.OnExecuteOrder -= this.OnExecuteOrder;
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (this.OrbwalkSleeper.IsSleeping)
            {
                return false;
            }

            if (this.Owner.HasModifier("modifier_pangolier_gyroshell") && target != null)
            {
                if (this.Owner.GetAngle(target.Position) > 1.5f)
                {
                    var position = this.thunder.GetPosition(target);

                    if (!position.IsZero && this.Owner.BaseUnit.Move(position))
                    {
                        this.OrbwalkSleeper.Sleep(1f);
                        return true;
                    }
                }

                if (this.Owner.BaseUnit.Move(target.Position))
                {
                    this.OrbwalkSleeper.Sleep(0.5f);
                    return true;
                }
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }

        protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBlinks(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.moveSwashbuckle))
            {
                return true;
            }

            return false;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (args.Ability?.Id == AbilityId.pangolier_swashbuckle && this.Owner.HasModifier("modifier_axe_berserkers_call"))
                {
                    //dota bug fix
                    args.Process = false;
                    return;
                }

                if (!args.Process || !args.IsPlayerInput || args.OrderId != OrderId.Ability
                    || args.Ability.Id != AbilityId.pangolier_gyroshell)
                {
                    return;
                }

                if (this.thunder == null)
                {
                    return;
                }

                this.ultSleeper.Sleep(this.thunder.Ability.GetCastDelay() + 0.15f);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}