namespace O9K.AIO.Heroes.Windranger.Units
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

    using BaseWindranger = Core.Entities.Heroes.Unique.Windranger;

    [UnitName(nameof(HeroId.npc_dota_hero_windrunner))]
    internal class Windranger : ControllableUnit, IDisposable
    {
        private readonly BaseWindranger windranger;

        private BlinkDaggerWindranger blink;

        private DisableAbility bloodthorn;

        private FocusFire focusFire;

        //private ForceStaff force;

        private DisableAbility hex;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private Powershot powershot;

        private Shackleshot shackleshot;

        private Windrun windrun;

        private MoveBuffAbility windrunMove;

        public Windranger(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.windranger = owner as BaseWindranger;

            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                {
                    AbilityId.windrunner_shackleshot, x =>
                        {
                            this.shackleshot = new Shackleshot(x);
                            if (this.powershot != null)
                            {
                                this.powershot.Shackleshot = this.shackleshot;
                            }

                            return this.shackleshot;
                        }
                },
                {
                    AbilityId.windrunner_powershot, x =>
                        {
                            this.powershot = new Powershot(x);
                            if (this.shackleshot != null)
                            {
                                this.powershot.Shackleshot = this.shackleshot;
                            }

                            return this.powershot;
                        }
                },
                { AbilityId.windrunner_windrun, x => this.windrun = new Windrun(x) },
                { AbilityId.windrunner_focusfire, x => this.focusFire = new FocusFire(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkDaggerWindranger(x) },
                //{ AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.windrunner_shackleshot, _ => this.shackleshot);
            this.MoveComboAbilities.Add(AbilityId.windrunner_windrun, x => this.windrunMove = new MoveBuffAbility(x));
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (this.powershot.CancelChanneling(targetManager))
            {
                this.ComboSleeper.Sleep(0.1f);
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.shackleshot))
            {
                abilityHelper.ForceUseAbility(this.shackleshot);
                this.OrbwalkSleeper.Sleep(0.5f);
                return true;
            }

            if (!abilityHelper.CanBeCasted(this.shackleshot, false, false))
            {
                if (abilityHelper.UseAbility(this.blink, this.Owner.GetAttackRange(targetManager.Target) + 100, 400))
                {
                    return true;
                }
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

            if (abilityHelper.UseAbility(this.shackleshot))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.shackleshot, false))
            {
                var castWindrun = abilityHelper.CanBeCasted(this.windrun, false, false);
                var movePosition = this.shackleshot.GetMovePosition(targetManager, comboModeMenu, castWindrun);
                //if ((movePosition.IsZero  || Owner.Distance(movePosition) > 300 || targetManager.Target.IsMoving) && abilityHelper.CanBeCasted(this.force, false, false))
                //{
                //    var forcePosition = this.shackleshot.GetForceStaffPosition(targetManager, this.force);
                //    if (!forcePosition.IsZero)
                //    {
                //        if (Owner.GetAngle(forcePosition) > 0.1)
                //        {
                //            this.Move(forcePosition);
                //            OrbwalkSleeper.Sleep(0.1f);
                //            this.ComboSleeper.Sleep(0.1f);
                //            return true;
                //        }

                //        abilityHelper.ForceUseAbility(this.force, true);
                //        return true;
                //    }
                //}

                if (!movePosition.IsZero && this.Move(movePosition))
                {
                    if ((this.Owner.Distance(movePosition) > 100 || targetManager.Target.IsMoving) && castWindrun)
                    {
                        abilityHelper.ForceUseAbility(this.windrun);
                    }

                    this.OrbwalkSleeper.Sleep(0.1f);
                    this.ComboSleeper.Sleep(0.1f);
                    return true;
                }
            }

            if (abilityHelper.UseAbilityIfCondition(this.powershot, this.shackleshot, this.blink))
            {
                this.ComboSleeper.ExtendSleep(0.2f);
                this.OrbwalkSleeper.ExtendSleep(0.2f);
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.focusFire, this.powershot))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.windrun))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }

        public virtual bool DirectionalMove()
        {
            if (this.OrbwalkSleeper.IsSleeping)
            {
                return false;
            }

            if (this.CanMove())
            {
                this.OrbwalkSleeper.Sleep(0.2f);
                return this.Owner.BaseUnit.MoveToDirection(Game.MousePosition);
            }

            return false;
        }

        public void Dispose()
        {
            this.powershot.Dispose();
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            var focusFireTarget = this.windranger?.FocusFireActive == true && this.windranger.FocusFireTarget?.Equals(target) == true;
            if (focusFireTarget && target.IsReflectingDamage)
            {
                return this.DirectionalMove();
            }

            if (focusFireTarget && this.Owner.HasModifier("modifier_windrunner_windrun_invis"))
            {
                focusFireTarget = false;
            }

            return base.Orbwalk(target, !focusFireTarget, move, comboMenu);
        }

        protected override bool MoveComboUseBuffs(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBuffs(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.windrunMove))
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

            if (abilityHelper.UseAbility(this.shackleshot))
            {
                return true;
            }

            return false;
        }
    }
}