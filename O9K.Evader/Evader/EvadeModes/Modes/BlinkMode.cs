namespace O9K.Evader.Evader.EvadeModes.Modes
{
    using System.Collections.Generic;

    using Abilities.Base.Usable.BlinkAbility;

    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class BlinkMode : EvadeBaseMode
    {
        private readonly List<BlinkAbility> abilities;

        public BlinkMode(IActionManager actionManager, List<BlinkAbility> abilities)
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
                Mode = EvadeMode.Blink,
                ObstacleId = obstacle.Id
            };

            if (ally.IsInvulnerable || (!obstacle.EvadableAbility.Ability.CanHitSpellImmuneEnemy && ally.IsMagicImmune))
            {
                result.State = EvadeResult.EvadeState.Ignore;
                return result;
            }

            if (!ally.IsImportant)
            {
                result.State = EvadeResult.EvadeState.Ignore;
                return result;
            }

            var enemy = obstacle.Caster;

            foreach (var id in obstacle.GetBlinks(ally))
            {
                foreach (var blinkAbility in this.abilities)
                {
                    if (blinkAbility.Ability.Id != id || !blinkAbility.CanBeCasted(ally, enemy, obstacle))
                    {
                        continue;
                    }

                    if (this.ActionManager.IsInputBlocked(blinkAbility.Owner))
                    {
                        continue;
                    }

                    var requiredTime = blinkAbility.GetRequiredTime(ally, enemy, obstacle);
                    var remainingTime = obstacle.GetEvadeTime(ally, true);

                    if (remainingTime - requiredTime <= 0)
                    {
                        continue;
                    }

                    var ignoreTime = obstacle.EvadableAbility.IgnoreRemainingTime(obstacle, blinkAbility);

                    if (!ignoreTime && remainingTime - requiredTime > EvadeTiming)
                    {
                        result.State = EvadeResult.EvadeState.TooEarly;
                        return result;
                    }

                    if (!this.ChannelCanceled(ally, obstacle, remainingTime, blinkAbility))
                    {
                        continue;
                    }

                    if (!this.PhaseCanceled(ally, obstacle, blinkAbility))
                    {
                        continue;
                    }

                    if (!blinkAbility.Use(ally, enemy, obstacle))
                    {
                        continue;
                    }

                    this.ActionManager.BlockInput(blinkAbility.Owner, obstacle, requiredTime + 0.1f);
                    this.ActionManager.IgnoreObstacle(ally, obstacle, remainingTime + 0.5f);

                    result.AbilityOwner = blinkAbility.Owner.DisplayName;
                    result.AllyAbility = blinkAbility.Ability.DisplayName;
                    result.State = EvadeResult.EvadeState.Successful;

                    return result;
                }
            }

            result.State = EvadeResult.EvadeState.Failed;
            return result;
        }
    }
}