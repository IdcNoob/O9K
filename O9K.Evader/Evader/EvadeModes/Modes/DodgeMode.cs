namespace O9K.Evader.Evader.EvadeModes.Modes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities.Base.Usable.DodgeAbility;

    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Menu.EventArgs;

    using Ensage;

    using Metadata;

    using Pathfinder;
    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    using SharpDX;

    internal class DodgeMode : EvadeBaseMode
    {
        private readonly List<DodgeAbility> dodgeAbilities;

        private readonly IPathfinder pathfinder;

        private Pathfinder.EvadeMode pathfinderMode;

        public DodgeMode(IActionManager actionManager, List<DodgeAbility> dodgeAbilities, IPathfinder pathfinder, IMainMenu menu)
            : base(actionManager)
        {
            this.dodgeAbilities = dodgeAbilities;
            this.pathfinder = pathfinder;

            menu.Hotkeys.PathfinderMode.ValueChange += this.PathfinderModeOnValueChanged;
        }

        public override EvadeResult Evade(Unit9 ally, IObstacle obstacle)
        {
            var result = new EvadeResult
            {
                Ally = ally.DisplayName,
                EnemyAbility = obstacle.EvadableAbility.Ability.DisplayName,
                IsModifier = obstacle.IsModifierObstacle,
                Mode = EvadeMode.Dodge,
                ObstacleId = obstacle.Id
            };

            if (!obstacle.CanBeDodged || ally.IsCourier)
            {
                result.State = EvadeResult.EvadeState.Ignore;
                return result;
            }

            if (this.pathfinderMode == Pathfinder.EvadeMode.None)
            {
                result.State = EvadeResult.EvadeState.Ignore;
                return result;
            }

            if ((this.pathfinderMode == Pathfinder.EvadeMode.Disables || ally.IsCharging) && !obstacle.EvadableAbility.Ability.IsDisable())
            {
                result.State = EvadeResult.EvadeState.Ignore;
                return result;
            }

            if (!ally.CanUseAbilities)
            {
                result.State = EvadeResult.EvadeState.Ignore;
                return result;
            }

            if (ally.IsInvulnerable || (!obstacle.EvadableAbility.Ability.CanHitSpellImmuneEnemy && ally.IsMagicImmune))
            {
                result.State = EvadeResult.EvadeState.Successful;
                return result;
            }

            if (!ally.CanMove(false))
            {
                result.State = EvadeResult.EvadeState.Failed;
                return result;
            }

            if (!ally.IsControllable || this.ActionManager.IsInputBlocked(ally))
            {
                result.State = EvadeResult.EvadeState.Failed;
                return result;
            }

            var remainingTime = obstacle.GetEvadeTime(ally, true) - (Game.Ping / 2000f);
            if (remainingTime <= 0)
            {
                result.State = EvadeResult.EvadeState.Failed;
                return result;
            }

            var movingIntoObstacle = !obstacle.IsIntersecting(ally, false);
            var path = movingIntoObstacle
                           ? this.pathfinder.GetPathFromObstacle(ally, ally.Speed, ally.InFront(100), 69, out var success)
                           : this.pathfinder.GetPathFromObstacle(ally, ally.Speed, ally.Position, 69, out success);

            if (!success)
            {
                result.State = EvadeResult.EvadeState.Failed;
                return result;
            }

            var ignoreTime = obstacle.EvadableAbility.IgnoreRemainingTime(obstacle, null);
            var movePosition = path.Last();
            var requiredTime = this.GetRequiredTime(ally, movePosition, remainingTime, out var speedBuffAbility) - 0.15f;

            if (obstacle is AreaOfEffectObstacle)
            {
                remainingTime -= 0.15f;
            }

            if (!ignoreTime)
            {
                if (requiredTime > remainingTime)
                {
                    result.State = EvadeResult.EvadeState.Failed;
                    return result;
                }

                if (requiredTime + 0.15 < remainingTime)
                {
                    if (EvadeModeManager.MoveCamera && !Hud.IsPositionOnScreen(movePosition))
                    {
                        Hud.CameraPosition = movePosition;
                    }

                    result.State = EvadeResult.EvadeState.TooEarly;
                    return result;
                }
            }

            if (ally.IsRuptured && ally.Distance(movePosition) > 300)
            {
                result.State = EvadeResult.EvadeState.Failed;
                return result;
            }

            if (!this.ChannelCanceled(ally, obstacle, remainingTime, null))
            {
                result.State = EvadeResult.EvadeState.Failed;
                return result;
            }

            if (!this.PhaseCanceled(ally, obstacle, null))
            {
                result.State = EvadeResult.EvadeState.Failed;
                return result;
            }

            if (speedBuffAbility != null)
            {
                speedBuffAbility.Use(ally, null, null);
                result.AllyAbility = speedBuffAbility.Ability.DisplayName;
            }

            if (EvadeModeManager.MoveCamera && !Hud.IsPositionOnScreen(movePosition))
            {
                Hud.CameraPosition = movePosition;
            }

            ally.BaseUnit.Move(movePosition, false, true);

            this.ActionManager.BlockInput(ally, obstacle, requiredTime + 0.2f);
            this.ActionManager.IgnoreObstacle(ally, obstacle, requiredTime + 0.5f);

            result.State = EvadeResult.EvadeState.Successful;
            return result;
        }

        private float GetRequiredTime(Unit9 ally, Vector3 movePosition, float remainingTime, out DodgeAbility speedBuffAbility)
        {
            var turnTime = ally.GetTurnTime(movePosition);
            var distance = ally.Distance(movePosition);
            var speed = ally.Speed;

            var requiredTime = turnTime + (distance / speed);
            if (remainingTime > requiredTime)
            {
                speedBuffAbility = null;
                return requiredTime;
            }

            speedBuffAbility = this.dodgeAbilities.Where(x => x.CanBeCasted(ally, null, null))
                .OrderByDescending(x => x.SpeedBuffAbility.GetSpeedBuff(ally))
                .FirstOrDefault();

            if (speedBuffAbility == null)
            {
                return requiredTime;
            }

            return turnTime + (distance / (speed + speedBuffAbility.SpeedBuffAbility.GetSpeedBuff(ally)));
        }

        private void PathfinderModeOnValueChanged(object sender, KeyEventArgs e)
        {
            if (!e.NewValue)
            {
                return;
            }

            if ((int)this.pathfinderMode >= Enum.GetNames(typeof(Pathfinder.EvadeMode)).Length - 1)
            {
                this.pathfinderMode = 0;
            }
            else
            {
                this.pathfinderMode++;
            }
        }
    }
}