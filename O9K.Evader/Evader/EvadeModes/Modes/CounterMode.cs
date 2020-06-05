namespace O9K.Evader.Evader.EvadeModes.Modes
{
    using System.Collections.Generic;

    using Abilities.Base.Usable.CounterAbility;

    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class CounterMode : EvadeBaseMode
    {
        private readonly List<CounterAbility> abilities;

        public CounterMode(IActionManager actionManager, List<CounterAbility> abilities)
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
                Mode = EvadeMode.Counter,
                ObstacleId = obstacle.Id
            };

            if (ally.IsInvulnerable || (!obstacle.EvadableAbility.Ability.CanHitSpellImmuneEnemy && ally.IsMagicImmune))
            {
                result.State = EvadeResult.EvadeState.Ignore;
                return result;
            }

            if (!ally.IsImportant && !ally.IsCourier)
            {
                result.State = EvadeResult.EvadeState.Ignore;
                return result;
            }

            //todo check linkens etc.

            var enemy = obstacle.Caster;

            foreach (var abilityId in obstacle.GetCounters(ally))
            {
                foreach (var counterAbility in this.abilities)
                {
                    if (counterAbility.Ability.Id != abilityId)
                    {
                        continue;
                    }

                    if (ally.IsCourier && !counterAbility.Owner.IsCourier)
                    {
                        continue;
                    }

                    if (!counterAbility.CanBeCasted(ally, enemy, obstacle))
                    {
                        continue;
                    }

                    if (this.ActionManager.IsInputBlocked(counterAbility.Owner))
                    {
                        continue;
                    }

                    var requiredTime = counterAbility.GetRequiredTime(ally, enemy, obstacle);
                    var remainingTime = obstacle.GetEvadeTime(ally, false);

                    if (remainingTime - requiredTime <= 0)
                    {
                        continue;
                    }

                    var ignoreTime = obstacle.EvadableAbility.IgnoreRemainingTime(obstacle, counterAbility);

                    if (!ignoreTime && remainingTime - requiredTime > EvadeTiming)
                    {
                        result.State = EvadeResult.EvadeState.TooEarly;
                        return result;
                    }

                    if (!this.ChannelCanceled(ally, obstacle, remainingTime, counterAbility))
                    {
                        continue;
                    }

                    if (!this.PhaseCanceled(ally, obstacle, counterAbility))
                    {
                        continue;
                    }

                    if (!counterAbility.Use(ally, enemy, obstacle))
                    {
                        continue;
                    }

                    if (counterAbility.BlockPlayersInput(obstacle))
                    {
                        if (counterAbility.IsToggleable)
                        {
                            this.ActionManager.BlockInput(counterAbility.Ability, obstacle, remainingTime + 0.1f);
                        }
                        else
                        {
                            this.ActionManager.BlockInput(counterAbility.Owner, obstacle, requiredTime + 0.1f);
                        }
                    }

                    this.ActionManager.IgnoreObstacle(ally, obstacle, remainingTime + 0.5f);

                    result.AbilityOwner = counterAbility.Owner.DisplayName;
                    result.AllyAbility = counterAbility.Ability.DisplayName;
                    result.State = EvadeResult.EvadeState.Successful;

                    return result;
                }
            }

            result.State = EvadeResult.EvadeState.Failed;
            return result;
        }
    }
}