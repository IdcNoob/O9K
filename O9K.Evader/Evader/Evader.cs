namespace O9K.Evader.Evader
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Logger;
    using Core.Managers.Assembly;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Prediction.Collision;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using EvadeModes;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Abilities.LinearProjectile;

    internal class Evader : IEvaderService
    {
        private readonly IActionManager actionManager;

        private readonly List<Unit9> allyUnits = new List<Unit9>();

        private readonly IAssemblyEventManager9 assemblyEventManager;

        private readonly IDebugger debugger;

        private readonly IEvadeModeManager evadeModeManager;

        private readonly IMainMenu menu;

        private readonly IPathfinder pathfinder;

        private Owner owner;

        private IUpdateHandler updateHandler;

        [ImportingConstructor]
        public Evader(
            IContext9 context,
            IEvadeModeManager evadeModeManager,
            IPathfinder pathfinder,
            IActionManager actionManager,
            IMainMenu menu,
            IDebugger debugger)
        {
            this.assemblyEventManager = context.AssemblyEventManager;
            this.evadeModeManager = evadeModeManager;
            this.pathfinder = pathfinder;
            this.actionManager = actionManager;
            this.menu = menu;
            this.debugger = debugger;
        }

        public LoadOrder LoadOrder { get; } = LoadOrder.Evader;

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            EntityManager9.UnitAdded += this.OnUnitAdded;
            EntityManager9.UnitRemoved += this.OnUnitRemoved;
            this.updateHandler = UpdateManager.Subscribe(this.OnUpdate, 0, false);
            this.pathfinder.AbilityCanceled += this.OnAbilityCanceled;
            this.pathfinder.ObstacleAdded += this.OnObstacleAdded;
        }

        public void Dispose()
        {
            UpdateManager.Unsubscribe(this.updateHandler);
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            this.pathfinder.ObstacleAdded -= this.OnObstacleAdded;
            this.pathfinder.AbilityCanceled -= this.OnAbilityCanceled;
        }

        private List<Unit9> GetUnits()
        {
            var units = new List<Unit9>(this.allyUnits.Count)
            {
                this.owner
            };

            foreach (var ally in this.allyUnits /*.OrderBy(x => x.Health)*/)
            {
                if (!ally.IsValid || !ally.IsAlive || ally.IsMyHero)
                {
                    continue;
                }

                if ((this.menu.Settings.MultiUnitControl && ally.IsControllable)
                    || (this.menu.AllySettings.HelpAllies && ally.IsImportant && this.menu.AllySettings.IsEnabled(ally.Name)))
                {
                    units.Add(ally);
                }
            }

            return units.OrderBy(x => this.menu.AllySettings.GetOrder(x)).ToList();
        }

        private void OnAbilityCanceled(object sender, IObstacle obstacle)
        {
            try
            {
                this.actionManager.UnblockInput(obstacle);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnObstacleAdded(object sender, bool added)
        {
            this.updateHandler.IsEnabled = added;
        }

        private void OnUnitAdded(Unit9 unit)
        {
            try
            {
                if ((!unit.IsUnit && !unit.IsCourier) || (!unit.IsControllable && !unit.IsImportant) || !unit.IsAlly(this.owner.Team))
                {
                    return;
                }

                this.allyUnits.Add(unit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitRemoved(Unit9 unit)
        {
            try
            {
                if (!unit.IsUnit || !unit.IsAlly(this.owner.Team))
                {
                    return;
                }

                this.allyUnits.Remove(unit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdate()
        {
            if (Game.IsPaused)
            {
                return;
            }

            try
            {
                var units = this.GetUnits();

                foreach (var unit in units)
                {
                    foreach (var obstacle in this.pathfinder.GetIntersectingObstacles(unit))
                    {
                        if (!obstacle.EvadableAbility.Enabled)
                        {
                            continue;
                        }

                        if (this.actionManager.IsObstacleIgnored(unit, obstacle)
                            || obstacle.EvadableAbility.IsObstacleIgnored(unit, obstacle))
                        {
                            continue;
                        }

                        EvadeResult evadeResult = null;

                        foreach (var mode in this.evadeModeManager.GetEvadeModes(obstacle))
                        {
                            evadeResult = mode.Evade(unit, obstacle);

                            if (evadeResult.State == EvadeResult.EvadeState.Successful
                                || evadeResult.State == EvadeResult.EvadeState.TooEarly)
                            {
                                break;
                            }
                        }

                        if (this.TryToBlock(unit, units, obstacle, evadeResult))
                        {
                            // ReSharper disable once PossibleNullReferenceException
                            evadeResult.Mode = EvadeMode.Dodge;
                            evadeResult.State = EvadeResult.EvadeState.Successful;
                        }

                        if (this.TryToSpendGold(unit, obstacle, evadeResult))
                        {
                            // ReSharper disable once PossibleNullReferenceException
                            evadeResult.Mode = EvadeMode.GoldSpend;
                            evadeResult.State = EvadeResult.EvadeState.Successful;
                        }

                        this.debugger.AddEvadeResult(evadeResult);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private bool TryToBlock(Unit9 unit, IEnumerable<Unit9> units, IObstacle obstacle, EvadeResult evadeResult)
        {
            if (evadeResult?.State != EvadeResult.EvadeState.Failed)
            {
                return false;
            }

            if (!(obstacle is LinearProjectileObstacle linearObstacle))
            {
                return false;
            }

            if ((obstacle.EvadableAbility.ActiveAbility.CollisionTypes & CollisionTypes.EnemyUnits) == 0)
            {
                return false;
            }

            foreach (var otherUnit in units.Where(x => !x.Equals(unit) && x.IsUnit && !x.IsImportant && !x.IsInvulnerable && x.CanMove()))
            {
                if ((otherUnit.UnitState & UnitState.NoCollision) != 0)
                {
                    continue;
                }

                var otherUnitPosition = otherUnit.Position.To2D();
                var enemyPosition = linearObstacle.Position.Extend2D(unit.Position, 75).To2D();
                var unitPosition = unit.Position.Extend2D(linearObstacle.Position, 75).To2D();

                var projection = Geometry.ProjectOn(otherUnitPosition, enemyPosition, unitPosition);

                var movePosition = projection.IsOnSegment
                                       ? projection.LinePoint.ToVector3()
                                           .Extend2D(otherUnit.Position, obstacle.EvadableAbility.ActiveAbility.Radius * 0.75f)
                                       : projection.SegmentPoint.ToVector3();

                var requiredTime = otherUnit.GetTurnTime(movePosition) + (otherUnit.Distance(movePosition) / otherUnit.Speed);

                if (linearObstacle.GetEvadeTime(movePosition) < requiredTime)
                {
                    continue;
                }

                if (otherUnit.BaseUnit.Move(movePosition, false, true))
                {
                    this.actionManager.BlockInput(otherUnit, obstacle, requiredTime);
                    this.actionManager.IgnoreObstacle(unit, obstacle, obstacle.GetEvadeTime(unit, false));

                    return true;
                }
            }

            return false;
        }

        private bool TryToSpendGold(Unit9 unit, IObstacle obstacle, EvadeResult evadeResult)
        {
            if (evadeResult?.State != EvadeResult.EvadeState.Failed)
            {
                return false;
            }

            if (!this.menu.AbilitySettings.IsGoldSpenderEnabled())
            {
                return false;
            }

            if (!unit.IsMyHero || obstacle.IsModifierObstacle)
            {
                return false;
            }

            var damage = obstacle.GetDamage(unit);
            if (damage < unit.Health)
            {
                return false;
            }

            this.assemblyEventManager.InvokeEvaderPredictedDeath();
            return true;
        }
    }
}