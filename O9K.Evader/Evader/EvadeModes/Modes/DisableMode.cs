namespace O9K.Evader.Evader.EvadeModes.Modes
{
    using System.Collections.Generic;

    using Abilities.Base.Usable.DisableAbility;

    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class DisableMode : EvadeBaseMode
    {
        private readonly List<DisableAbility> abilities;

        public DisableMode(IActionManager actionManager, List<DisableAbility> abilities)
            : base(actionManager)
        {
            this.abilities = abilities;
        }

        public override EvadeResult Evade(Unit9 ally, IObstacle obstacle)
        {
            var result = new EvadeResult
            {
                Ally = ally.DisplayName,
                EnemyAbility = obstacle.EvadableAbility.Ability.DisplayName,
                IsModifier = obstacle.IsModifierObstacle,
                Mode = EvadeMode.Disable,
                ObstacleId = obstacle.Id
            };

            var enemy = obstacle.Caster;
            if (enemy.IsInvulnerable)
            {
                result.State = EvadeResult.EvadeState.Failed;
                return result;
            }

            if (!ally.IsImportant)
            {
                result.State = EvadeResult.EvadeState.Ignore;
                return result;
            }

            foreach (var id in obstacle.GetDisables(ally))
            {
                foreach (var disableAbility in this.abilities)
                {
                    if (disableAbility.Ability.Id != id)
                    {
                        continue;
                    }

                    if (!disableAbility.CanBeCasted(ally, enemy, obstacle))
                    {
                        continue;
                    }

                    if (this.ActionManager.IsInputBlocked(disableAbility.Owner))
                    {
                        continue;
                    }

                    var requiredTime = disableAbility.GetRequiredTime(ally, enemy, obstacle);
                    var remainingTime = obstacle.GetDisableTime(enemy);

                    if (remainingTime - requiredTime <= 0)
                    {
                        continue;
                    }

                    var ignoreTime = obstacle.EvadableAbility.IgnoreRemainingTime(obstacle, disableAbility);

                    if (!ignoreTime && remainingTime - requiredTime > EvadeTiming)
                    {
                        result.State = EvadeResult.EvadeState.TooEarly;
                        return result;
                    }

                    //todo remove/change this ?
                    //if ((enemy.UnitState & disableAbility.AppliesUnitState) != 0)
                    //{
                    //    result.State = EvadeResult.EvadeState.Ignore;
                    //    return result;
                    //}

                    if (!this.ChannelCanceled(ally, obstacle, remainingTime, disableAbility))
                    {
                        continue;
                    }

                    if (!this.PhaseCanceled(ally, obstacle, disableAbility))
                    {
                        continue;
                    }

                    if (!disableAbility.Use(ally, enemy, obstacle))
                    {
                        continue;
                    }

                    this.ActionManager.BlockInput(disableAbility.Owner, obstacle, requiredTime + 0.1f);
                    this.ActionManager.IgnoreObstacle(obstacle, remainingTime + 0.5f);

                    result.AbilityOwner = disableAbility.Owner.DisplayName;
                    result.AllyAbility = disableAbility.Ability.DisplayName;
                    result.Enemy = enemy.DisplayName;
                    result.State = EvadeResult.EvadeState.Successful;

                    return result;
                }
            }

            result.State = EvadeResult.EvadeState.Failed;
            return result;
        }
    }
}