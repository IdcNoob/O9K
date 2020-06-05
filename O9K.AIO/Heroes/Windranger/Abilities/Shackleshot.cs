namespace O9K.AIO.Heroes.Windranger.Abilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Abilities;
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    using BaseShackleshot = Core.Entities.Abilities.Heroes.Windranger.Shackleshot;

    internal class Shackleshot : DisableAbility
    {
        private readonly BaseShackleshot shackleshot;

        private Unit9 shackleTarget;

        public Shackleshot(ActiveAbility ability)
            : base(ability)
        {
            this.shackleshot = (BaseShackleshot)ability;
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var target = targetManager.Target;

            if (target.IsMagicImmune && !this.shackleshot.CanHitSpellImmuneEnemy)
            {
                return false;
            }

            if (this.Owner.Distance(target) > this.shackleshot.CastRange + this.shackleshot.ShackleRange)
            {
                return false;
            }

            var mainInput = this.shackleshot.GetPredictionInput(target);
            mainInput.Range = this.shackleshot.CastRange;
            var mainOutput = this.shackleshot.PredictionManager.GetSimplePrediction(mainInput);
            var targetPredictedPosition = mainOutput.TargetPosition;
            var ownerPosition = this.Owner.Position;
            var targetPosition = target.Position;

            if (!target.IsBlockingAbilities)
            {
                foreach (var unit in targetManager.AllEnemyUnits)
                {
                    // target => unit

                    if (unit.Equals(target) || unit.IsMagicImmune || unit.Distance(target) > this.shackleshot.ShackleRange)
                    {
                        continue;
                    }

                    var input = this.shackleshot.GetPredictionInput(unit);
                    input.Delay -= target.Distance(unit) / this.shackleshot.Speed;
                    var output = this.shackleshot.PredictionManager.GetSimplePrediction(input);
                    var unitPredictedPosition = output.TargetPosition;
                    var unitPosition = unit.Position;

                    if (unitPredictedPosition.Distance(targetPredictedPosition) > this.shackleshot.ShackleRange)
                    {
                        continue;
                    }

                    var predictedAngle =
                        (targetPredictedPosition - ownerPosition).AngleBetween(unitPredictedPosition - targetPredictedPosition);
                    var angle = (targetPosition - ownerPosition).AngleBetween(unitPosition - targetPosition);

                    if ((predictedAngle < this.shackleshot.Angle && angle < this.shackleshot.Angle)
                        || predictedAngle < this.shackleshot.Angle / 2)
                    {
                        this.shackleTarget = target;
                        return true;
                    }
                }
            }

            foreach (var unit in targetManager.AllEnemyUnits)
            {
                // unit => target

                if (unit.Equals(target) || unit.IsMagicImmune)
                {
                    continue;
                }

                if (unit.Distance(target) > this.shackleshot.ShackleRange || unit.Distance(targetPredictedPosition) < 50)
                {
                    continue;
                }

                var input = this.shackleshot.GetPredictionInput(unit);
                var output = this.shackleshot.PredictionManager.GetSimplePrediction(input);
                var unitPredictedPosition = output.TargetPosition;
                var unitPosition = unit.Position;

                if (unitPredictedPosition.Distance(targetPredictedPosition) > this.shackleshot.ShackleRange)
                {
                    continue;
                }

                mainInput = this.shackleshot.GetPredictionInput(target);
                mainInput.Range = this.shackleshot.CastRange;
                mainInput.Delay -= target.Distance(unit) / this.shackleshot.Speed;
                mainOutput = this.shackleshot.PredictionManager.GetSimplePrediction(mainInput);
                targetPredictedPosition = mainOutput.TargetPosition;

                var predictedAngle = (unitPredictedPosition - ownerPosition).AngleBetween(targetPredictedPosition - unitPredictedPosition);
                var angle = (unitPosition - ownerPosition).AngleBetween(targetPosition - unitPosition);

                if ((predictedAngle < this.shackleshot.Angle && angle < this.shackleshot.Angle)
                    || predictedAngle < this.shackleshot.Angle / 2)
                {
                    this.shackleTarget = unit;
                    return true;
                }
            }

            if (!target.IsBlockingAbilities)
            {
                foreach (var tree in EntityManager9.Trees)
                {
                    if (tree.Distance2D(targetPredictedPosition) > this.shackleshot.ShackleRange)
                    {
                        continue;
                    }

                    var predictedAngle = (targetPredictedPosition - ownerPosition).AngleBetween(tree.Position - targetPredictedPosition);
                    var angle = (targetPosition - ownerPosition).AngleBetween(tree.Position - targetPosition);

                    if ((predictedAngle < this.shackleshot.Angle && angle < this.shackleshot.Angle)
                        || predictedAngle < this.shackleshot.Angle / 2)
                    {
                        this.shackleTarget = target;
                        return true;
                    }
                }
            }

            return false;
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new ShackleshotMenu(this.Ability, simplifiedName);
        }

        public Vector3 GetBlinkPosition(TargetManager targetManager, float range)
        {
            var target = targetManager.Target;
            if (target.IsMagicImmune || target.IsEthereal || target.IsInvulnerable || !target.IsVisible || target.IsBlockingAbilities)
            {
                return Vector3.Zero;
            }

            var mainInput = this.shackleshot.GetPredictionInput(target);
            mainInput.Range = this.shackleshot.CastRange;
            mainInput.Delay -= Math.Max((this.Owner.Distance(target) - 200) / this.Ability.Speed, 0);
            var mainOutput = this.shackleshot.PredictionManager.GetSimplePrediction(mainInput);
            var targetPosition = mainOutput.TargetPosition;

            foreach (var unit in targetManager.EnemyUnits.OrderByDescending(x => x.IsHero))
            {
                if (unit.Distance(targetPosition) < 50)
                {
                    continue;
                }

                if (unit.IsMagicImmune && !this.shackleshot.CanHitSpellImmuneEnemy)
                {
                    continue;
                }

                if (unit.Equals(target) || unit.IsInvulnerable)
                {
                    continue;
                }

                if (unit.Distance(targetPosition) > this.shackleshot.ShackleRange)
                {
                    continue;
                }

                var input = this.Ability.GetPredictionInput(unit);
                input.Delay = mainInput.Delay;
                var output = this.Ability.GetPredictionOutput(input);

                var position = output.TargetPosition.Extend2D(targetPosition, output.TargetPosition.Distance2D(targetPosition) + 200);
                if (this.Owner.Distance(position) < range)
                {
                    return position;
                }
            }

            if (!target.IsBlockingAbilities)
            {
                foreach (var tree in EntityManager9.Trees)
                {
                    if (tree.Distance2D(targetPosition) > this.shackleshot.ShackleRange)
                    {
                        continue;
                    }

                    var position = tree.Position.Extend2D(targetPosition, tree.Distance2D(targetPosition) + 200);
                    if (this.Owner.Distance(position) < range)
                    {
                        return position;
                    }
                }
            }

            return Vector3.Zero;
        }

        public Vector3 GetMovePosition(TargetManager targetManager, ComboModeMenu comboModeMenu, bool windrun)
        {
            var menu = comboModeMenu.GetAbilitySettingsMenu<ShackleshotMenu>(this);
            if (!menu.MoveToShackle)
            {
                return Vector3.Zero;
            }

            var target = targetManager.Target;

            if (target.IsMoving && this.Owner.Speed * (windrun ? 1.5f : 0.9f) < target.Speed)
            {
                return Vector3.Zero;
            }

            var possiblePositions = new List<Vector3>();
            var targetPosition = target.Position;

            foreach (var unit in targetManager.EnemyUnits)
            {
                if (unit.Equals(target))
                {
                    continue;
                }

                if (unit.Distance(target) > this.shackleshot.ShackleRange)
                {
                    continue;
                }

                var p1 = unit.Position.Extend2D(targetPosition, -200);
                var p2 = targetPosition.Extend2D(unit.Position, -200);

                if (this.Owner.Distance(p1) < 500 && !unit.IsBlockingAbilities)
                {
                    possiblePositions.Add(p1);
                }

                if (this.Owner.Distance(p2) < 500 && !target.IsBlockingAbilities)
                {
                    possiblePositions.Add(p2);
                }
            }

            if (!target.IsBlockingAbilities)
            {
                foreach (var tree in EntityManager9.Trees)
                {
                    if (targetPosition.Distance2D(tree.Position) > this.shackleshot.ShackleRange)
                    {
                        continue;
                    }

                    var p1 = targetPosition.Extend2D(tree.Position, -200);

                    if (this.Owner.Distance(p1) < 500)
                    {
                        possiblePositions.Add(p1);
                    }
                }
            }

            if (possiblePositions.Count > 0)
            {
                return possiblePositions.OrderBy(x => this.Owner.Distance(x)).First();
            }

            return Vector3.Zero;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (this.Ability.UnitTargetCast && !target.IsVisible)
            {
                return false;
            }

            if (target.IsDarkPactProtected)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                return false;
            }

            if (target.IsStunned)
            {
                return this.ChainStun(target, false);
            }

            if (target.IsHexed)
            {
                return this.ChainStun(target, false);
            }

            if (target.IsSilenced)
            {
                return !this.Disable.IsSilence(false) || this.ChainStun(target, false);
            }

            if (target.IsRooted)
            {
                return !this.Disable.IsRoot() || this.ChainStun(target, false);
            }

            if (target.IsRooted && !this.Ability.UnitTargetCast && target.GetImmobilityDuration() <= 0)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = this.shackleTarget ?? targetManager.Target;

            if (!this.Ability.UseAbility(target))
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(target) + 0.5f;
            var delay = this.Ability.GetCastDelay(target);

            target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            if (this.shackleTarget?.Equals(targetManager.Target) == false)
            {
                targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            }

            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            this.shackleTarget = null;

            return true;
        }
    }
}