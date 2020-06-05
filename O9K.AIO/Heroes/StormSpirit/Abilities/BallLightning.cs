namespace O9K.AIO.Heroes.StormSpirit.Abilities
{
    using System.Collections.Generic;

    using AIO.Abilities;
    using AIO.Abilities.Menus;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Geometry;

    using SharpDX;

    using TargetManager;

    using BaseBallLightning = Core.Entities.Abilities.Heroes.StormSpirit.BallLightning;

    internal class BallLightning : BlinkAbility
    {
        public enum Mode
        {
            Aggressive,

            Defensive
        }

        private readonly BaseBallLightning ball;

        private Vector3 ballPosition;

        private bool maxDamage;

        private float menuMaxCastRange;

        private Mode mode;

        public BallLightning(ActiveAbility ability)
            : base(ability)
        {
            this.ball = (BaseBallLightning)ability;
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var menu = comboMenu.GetAbilitySettingsMenu<BallLightningMenu>(this);
            if (menu != null)
            {
                this.menuMaxCastRange = menu.MaxCastRange;
                this.maxDamage = menu.MaxDamageCombo;
                this.mode = menu.Mode;
            }

            return true;
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            return this.UseAbility(targetManager, comboSleeper, this.Owner.IsMoving ? this.Owner.InFront(100) : this.Owner.InFront(25));
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new BallLightningMenu(this.Ability, simplifiedName);
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (this.Owner.IsInvulnerable)
            {
                return false;
            }

            if (targetManager.Target == null)
            {
                return true;
            }

            var target = targetManager.Target;

            if (target.HasModifier("modifier_pudge_meat_hook"))
            {
                return false;
            }

            return true;
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var target = targetManager.Target;
            var targetPosition = target.Position;
            var ownerPosition = this.Owner.Position;
            var distance = this.Owner.Distance(target);

            if (distance > this.menuMaxCastRange)
            {
                return false;
            }

            var attackRange = this.Owner.GetAttackRange(target);
            var isInAttackRange = distance < attackRange;
            var immobilityDuration = target.GetImmobilityDuration();
            var aggressive = this.mode == Mode.Aggressive;

            var maxRange = this.ball.MaxCastRange;
            var input = this.Ability.GetPredictionInput(targetManager.Target);
            input.CastRange = maxRange;
            input.Range = maxRange;
            input.Radius = 1;
            input.Delay += 0.3f;
            var output = this.Ability.GetPredictionOutput(input);
            var predictedPosition = output.CastPosition;

            if (aggressive)
            {
                var remnant = usableAbilities.Find(x => x.Ability.Id == AbilityId.storm_spirit_static_remnant);
                var vortex = usableAbilities.Find(x => x.Ability.Id == AbilityId.storm_spirit_electric_vortex);

                if (remnant != null && distance > 200)
                {
                    if (vortex != null && this.ball.GetRemainingMana(predictedPosition)
                        > remnant.Ability.ManaCost + vortex.Ability.ManaCost)
                    {
                        this.ballPosition = predictedPosition;
                        return true;
                    }

                    if (!target.HasModifier("modifier_storm_spirit_electric_vortex_pull"))
                    {
                        if (this.ball.GetRemainingMana(predictedPosition) > remnant.Ability.ManaCost)
                        {
                            this.ballPosition = predictedPosition;
                            return true;
                        }
                    }
                }

                if (!target.IsVisible)
                {
                    this.ballPosition = predictedPosition;
                    return true;
                }
            }

            if (isInAttackRange)
            {
                if (!this.maxDamage || !this.Owner.CanAttack(target))
                {
                    return false;
                }

                if (immobilityDuration > 0.5f)
                {
                    this.ballPosition = this.Owner.InFront(25);
                    return true;
                }

                if (aggressive || target.IsRanged || target.GetAngle(this.Owner) > 1f)
                {
                    var position1 = predictedPosition.Extend2D(ownerPosition, attackRange - 50);
                    var position2 = ownerPosition.Extend2D(
                        predictedPosition,
                        aggressive || this.Owner.IsMoving && !this.Owner.IsRotating ? 75 : 25);

                    this.ballPosition = predictedPosition.Distance2D(position1) < predictedPosition.Distance2D(position2)
                                            ? position1
                                            : position2;
                    return true;
                }
                else
                {
                    var position1 = targetPosition.Extend2D(ownerPosition, attackRange - 150);
                    var position2 = targetPosition.Extend2D(ownerPosition, 300);

                    var position = ownerPosition.Distance2D(position1) < ownerPosition.Distance2D(position2) ? position1 : position2;

                    if (this.Owner.Distance(position) < 50)
                    {
                        position = this.Owner.InFront(25);
                    }

                    this.ballPosition = position;
                    return true;
                }
            }

            this.ballPosition = predictedPosition.Extend2D(ownerPosition, attackRange - 100);
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            return this.UseAbility(targetManager, comboSleeper, this.ballPosition);
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
        {
            if (!this.Ability.UseAbility(toPosition))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(toPosition);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }
    }
}