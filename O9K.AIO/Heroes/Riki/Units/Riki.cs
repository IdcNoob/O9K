namespace O9K.AIO.Heroes.Riki.Units
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
    using Core.Extensions;
    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_riki))]
    internal class Riki : ControllableUnit
    {
        private DisableAbility abyssal;

        private DisableAbility atos;

        private BlinkAbility blink;

        private NukeAbility blinkStrike;

        private DisableAbility bloodthorn;

        private DebuffAbility diffusal;

        private DebuffAbility medallion;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private DisableAbility smoke;

        private DebuffAbility solar;

        private TricksOfTheTrade tricks;

        public Riki(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.riki_smoke_screen, x => this.smoke = new SmokeScreen(x) },
                { AbilityId.riki_blink_strike, x => this.blinkStrike = new NukeAbility(x) },
                { AbilityId.riki_tricks_of_the_trade, x => this.tricks = new TricksOfTheTrade(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
                { AbilityId.item_diffusal_blade, x => this.diffusal = new DebuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_solar_crest, x => this.solar = new DebuffAbility(x) },
                { AbilityId.item_medallion_of_courage, x => this.medallion = new DebuffAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (this.tricks?.CancelChanneling(targetManager) == true)
            {
                this.ComboSleeper.Sleep(0.1f);
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blinkStrike))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.abyssal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nullifier))
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

            if (abilityHelper.UseAbility(this.diffusal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.solar))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.medallion))
            {
                return true;
            }

            if (!abilityHelper.CanBeCasted(this.blinkStrike))
            {
                if (abilityHelper.UseAbility(this.blink, 500, 0))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbilityIfNone(this.smoke, this.blinkStrike))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.tricks))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }

        protected override bool ForceMove(Unit9 target, bool attack)
        {
            var mousePosition = Game.MousePosition;
            var movePosition = mousePosition;

            if (target != null && attack)
            {
                var targetPosition = target.Position;
                movePosition = target.InFront(100, 180, false);

                if (this.Menu.OrbwalkingMode == "Move to target" || this.CanAttack(target, 400))
                {
                    movePosition = targetPosition;
                }

                if (this.Menu.DangerRange > 0)
                {
                    var dangerRange = Math.Min((int)this.Owner.GetAttackRange(), this.Menu.DangerRange);
                    var targetDistance = this.Owner.Distance(target);

                    if (this.Menu.DangerMoveToMouse)
                    {
                        if (targetDistance < dangerRange)
                        {
                            movePosition = mousePosition;
                        }
                    }
                    else
                    {
                        if (targetDistance < dangerRange)
                        {
                            var angle = (targetPosition - this.Owner.Position).AngleBetween(movePosition - targetPosition);
                            if (angle < 90)
                            {
                                if (angle < 30)
                                {
                                    movePosition = targetPosition.Extend2D(movePosition, (dangerRange - 25) * -1);
                                }
                                else
                                {
                                    var difference = mousePosition - targetPosition;
                                    var rotation = difference.Rotated(MathUtil.DegreesToRadians(90));
                                    var end = rotation.Normalized() * (dangerRange - 25);
                                    var right = targetPosition + end;
                                    var left = targetPosition - end;

                                    movePosition = this.Owner.Distance(right) < this.Owner.Distance(left) ? right : left;
                                }
                            }
                            else if (target.Distance(movePosition) < dangerRange)
                            {
                                movePosition = targetPosition.Extend2D(movePosition, dangerRange - 25);
                            }
                        }
                    }
                }
            }

            if (movePosition == this.LastMovePosition)
            {
                return false;
            }

            if (!this.Owner.BaseUnit.Move(movePosition))
            {
                return false;
            }

            this.LastMovePosition = movePosition;
            return true;
        }
    }
}