namespace O9K.Core.Prediction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Collision;

    using Data;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using Entities.Units;

    using Extensions;

    using Managers.Entity;

    using SharpDX;

    public class PredictionManager9 : IPredictionManager9
    {
        //todo fix this shit prediction

        public PredictionOutput9 GetPrediction(PredictionInput9 input)
        {
            var output = this.GetSimplePrediction(input);

            if (input.AreaOfEffect)
            {
                this.GetAreaOfEffectPrediction(input, output);
            }
            else if (input.SkillShotType == SkillShotType.AreaOfEffect)
            {
                output.CastPosition = input.CastRange > 0 ? input.Caster.InFront(input.CastRange) : input.Caster.Position;
            }

            GetProperCastPosition(input, output);

            if (input.SkillShotType == SkillShotType.Line && !input.AreaOfEffect && input.UseBlink)
            {
                output.BlinkLinePosition = output.TargetPosition.Extend2D(input.Caster.Position, 200);
                output.CastPosition = output.TargetPosition;
            }

            if (!CheckRange(input, output))
            {
                return output;
            }

            this.CheckCollision(input, output);

            return output;
        }

        public PredictionOutput9 GetSimplePrediction(PredictionInput9 input)
        {
            var target = input.Target;
            var targetPosition = input.Target.Position;
            var caster = input.Caster;
            var targetIsVisible = target.IsVisible;
            var totalDelay = input.Delay;

            var output = new PredictionOutput9
            {
                Target = target
            };

            if (target.Equals(caster))
            {
                output.HitChance = HitChance.High;
                output.TargetPosition = targetPosition;
                output.CastPosition = targetPosition;
                return output;
            }

            if (input.RequiresToTurn)
            {
                totalDelay += caster.GetTurnTime(targetPosition);
            }

            if (input.Speed > 0)
            {
                totalDelay += caster.Distance(targetPosition) / input.Speed;
            }

            var predicted = target.GetPredictedPosition(totalDelay);
            output.TargetPosition = predicted;
            output.CastPosition = predicted;

            if (!targetIsVisible)
            {
                output.HitChance = HitChance.Low;
                return output;
            }

            if (target.IsStunned || target.IsRooted || target.IsHexed)
            {
                output.HitChance = HitChance.Immobile;
                return output;
            }

            if (!target.IsMoving && !caster.IsVisibleToEnemies)
            {
                output.HitChance = HitChance.High;
                return output;
            }

            output.HitChance = totalDelay > 0.5f ? HitChance.Medium : HitChance.High;
            return output;
        }

        private static bool CheckRange(PredictionInput9 input, PredictionOutput9 output)
        {
            if (input.Radius >= 9999999 || input.Range >= 9999999)
            {
                return true;
            }

            if (input.SkillShotType == SkillShotType.AreaOfEffect)
            {
                if (output.TargetPosition.Distance2D(output.CastPosition) > input.Radius)
                {
                    output.HitChance = HitChance.Impossible;
                    return false;
                }

                return true;
            }

            if (input.UseBlink && input.SkillShotType == SkillShotType.Line)
            {
                if (input.Caster.Distance(output.CastPosition) > input.CastRange + input.Range)
                {
                    output.HitChance = HitChance.Impossible;
                    return false;
                }

                return true;
            }

            if (input.Caster.Distance(output.CastPosition) > input.CastRange
                && (input.SkillShotType == SkillShotType.RangedAreaOfEffect || input.Caster.Distance(output.TargetPosition) > input.Range))
            {
                output.HitChance = HitChance.Impossible;
                return false;
            }

            return true;
        }

        private static void GetProperCastPosition(PredictionInput9 input, PredictionOutput9 output)
        {
            if (input.SkillShotType == SkillShotType.RangedAreaOfEffect || input.SkillShotType == SkillShotType.AreaOfEffect)
            {
                return;
            }

            if (input.SkillShotType == SkillShotType.Line && input.UseBlink)
            {
                return;
            }

            var radius = input.Radius;
            if (radius <= 0)
            {
                return;
            }

            var caster = input.Caster;
            var casterPosition = caster.Position;
            var castPosition = output.CastPosition;
            var distance = casterPosition.Distance2D(castPosition);
            var range = input.CastRange;

            if (range >= distance)
            {
                return;
            }

            castPosition = castPosition.Extend2D(casterPosition, Math.Min(distance - range, radius));

            if (output.AoeTargetsHit.Count > 1)
            {
                var maxDistance = output.AoeTargetsHit.Max(x => x.TargetPosition.Distance2D(castPosition));
                if (maxDistance > radius)
                {
                    distance = casterPosition.Distance2D(castPosition);
                    castPosition = casterPosition.Extend2D(castPosition, distance + (maxDistance - radius));
                }
            }

            output.CastPosition = castPosition;
        }

        private void CheckCollision(PredictionInput9 input, PredictionOutput9 output)
        {
            if (input.CollisionTypes != CollisionTypes.None)
            {
                var caster = input.Caster;
                var scanRange = caster.Distance(output.CastPosition);
                var movingObjects = new List<Unit9>();
                var collisionObjects = new List<CollisionObject>();
                var allUnits = EntityManager9.Units.Where(
                        x => x.IsUnit && !x.Equals(caster) && !x.Equals(input.Target) && x.IsAlive && x.IsVisible
                             && x.Distance(caster) < scanRange)
                    .ToList();

                if ((input.CollisionTypes & CollisionTypes.AllyCreeps) == CollisionTypes.AllyCreeps)
                {
                    movingObjects.AddRange(allUnits.Where(x => x.IsAlly(caster)));
                }

                if ((input.CollisionTypes & CollisionTypes.EnemyCreeps) == CollisionTypes.EnemyCreeps)
                {
                    movingObjects.AddRange(allUnits.Where(x => !x.IsAlly(caster)));
                }

                if ((input.CollisionTypes & CollisionTypes.AllyHeroes) == CollisionTypes.AllyHeroes)
                {
                    movingObjects.AddRange(allUnits.Where(x => x.IsHero && x.IsAlly(caster)));
                }

                if ((input.CollisionTypes & CollisionTypes.EnemyHeroes) == CollisionTypes.EnemyHeroes)
                {
                    movingObjects.AddRange(allUnits.Where(x => x.IsHero && !x.IsAlly(caster)));
                }

                foreach (var unit in movingObjects)
                {
                    var unitInput = new PredictionInput9
                    {
                        Target = unit,
                        Caster = input.Caster,
                        Delay = input.Delay,
                        Speed = input.Speed,
                        CastRange = input.CastRange,
                        Radius = input.Radius,
                        RequiresToTurn = input.RequiresToTurn
                    };

                    var predictedPos = this.GetSimplePrediction(unitInput);
                    collisionObjects.Add(new CollisionObject(unit, predictedPos.TargetPosition, unit.HullRadius + 10));
                }

                //todo runes ?
                //if ((input.CollisionTypes & CollisionTypes.Runes) == CollisionTypes.Runes)
                //{
                //    foreach (var rune in EntityManager<Rune>.Entities.Where(unit => input.Owner.IsInRange(unit, scanRange)))
                //    {
                //        collisionObjects.Add(new CollisionObject(rune, rune.NetworkPosition, 75f));
                //    }
                //}

                var collisionResult = Collision.Collision.GetCollision(
                    caster.Position.ToVector2(),
                    output.CastPosition.ToVector2(),
                    input.Radius,
                    collisionObjects);

                if (collisionResult.Collides)
                {
                    output.HitChance = HitChance.Impossible;
                }

                // output.CollisionResult = collisionResult;
            }
        }

        private void GetAreaOfEffectPrediction(PredictionInput9 input, PredictionOutput9 output)
        {
            var targets = new List<PredictionOutput9>();

            foreach (var target in input.AreaOfEffectTargets.Where(x => !x.Equals(output.Target)))
            {
                var aoeTargetInput = new PredictionInput9
                {
                    Target = target,
                    Caster = input.Caster,
                    Delay = input.Delay,
                    Speed = input.Speed,
                    CastRange = input.CastRange,
                    Radius = input.Radius,
                    RequiresToTurn = input.RequiresToTurn
                };

                var aoeTargetOutput = this.GetSimplePrediction(aoeTargetInput);
                var range = input.SkillShotType == SkillShotType.Line ? input.Range + input.CastRange : input.Range;

                if (input.Caster.Distance(aoeTargetOutput.CastPosition) < range)
                {
                    targets.Add(aoeTargetOutput);
                }
            }

            switch (input.SkillShotType)
            {
                case SkillShotType.RangedAreaOfEffect:
                {
                    targets.Insert(0, output);
                    output.CastPosition = input.Target.Position;
                    output.AoeTargetsHit = targets.Where(x => output.CastPosition.IsInRange(x.TargetPosition, input.Radius)).ToList();
                    if (!output.AoeTargetsHit.Contains(output))
                    {
                        output.AoeTargetsHit.Add(output);
                    }

                    break;
                }

                case SkillShotType.AreaOfEffect:
                {
                    targets.Insert(0, output);
                    output.CastPosition = input.CastRange > 0 ? input.Caster.InFront(input.CastRange) : input.Caster.Position;
                    output.AoeTargetsHit = targets.Where(x => output.CastPosition.IsInRange(x.TargetPosition, input.Radius)).ToList();
                    break;
                }

                case SkillShotType.Circle:
                {
                    targets.Insert(0, output);

                    if (targets.Count == 1)
                    {
                        output.AoeTargetsHit.Add(output);
                    }
                    else
                    {
                        while (targets.Count > 1)
                        {
                            var mecResult = MEC.GetMec(targets.Select(x => x.TargetPosition.ToVector2()).ToList());

                            if (mecResult.Radius > 0 && mecResult.Radius < input.Radius
                                                     && input.Caster.Distance(mecResult.Center.ToVector3()) < input.Range)
                            {
                                output.CastPosition = new Vector3(
                                    targets.Count <= 2
                                        ? (targets[0].TargetPosition.ToVector2() + targets[1].TargetPosition.ToVector2()) / 2
                                        : mecResult.Center,
                                    output.CastPosition.Z);
                                output.AoeTargetsHit = targets.Where(x => output.CastPosition.IsInRange(x.TargetPosition, input.Radius))
                                    .ToList();
                                break;
                            }

                            var itemToRemove = targets.MaxOrDefault(x => targets[0].TargetPosition.DistanceSquared(x.TargetPosition));
                            targets.Remove(itemToRemove);
                            output.AoeTargetsHit.Add(output);
                        }
                    }

                    break;
                }

                case SkillShotType.Cone:
                {
                    targets.Insert(0, output);

                    if (targets.Count > 1)
                    {
                        // yolo
                        var polygons = new Dictionary<Polygon.Trapezoid, List<PredictionOutput9>>();

                        if (input.UseBlink)
                        {
                            var targetPosition = output.TargetPosition;
                            var otherTargets = targets.Skip(1).ToList();

                            foreach (var predictionOutput9 in otherTargets)
                            {
                                var aoeTargetPosition = predictionOutput9.TargetPosition;
                                var averagePosition = (targetPosition + aoeTargetPosition) / 2;
                                var start = targetPosition.Extend2D(aoeTargetPosition, -100);
                                var end = start.Extend2D(aoeTargetPosition, input.Range);
                                var rec = new Polygon.Trapezoid(start, end, input.Radius, input.EndRadius);

                                foreach (var output9 in otherTargets)
                                {
                                    if (output9.Target == predictionOutput9.Target)
                                    {
                                        continue;
                                    }

                                    var averagePosition2 = (averagePosition + output9.TargetPosition) / 2;
                                    var start2 = targetPosition.Extend2D(averagePosition2, -100);
                                    var end2 = start2.Extend2D(averagePosition2, input.Range);
                                    var rec2 = new Polygon.Trapezoid(start2, end2, input.Radius + 50, input.EndRadius + 50);

                                    if (!rec2.IsInside(aoeTargetPosition) || !rec2.IsInside(output9.TargetPosition))
                                    {
                                        continue;
                                    }

                                    rec = rec2;
                                }

                                polygons[rec] = targets.Where(x => rec.IsInside(x.TargetPosition)).ToList();
                            }
                        }
                        else
                        {
                            var startPosition = input.Caster.Position;

                            foreach (var predictionOutput in targets)
                            {
                                var endPosition = startPosition.Extend2D(predictionOutput.TargetPosition, input.Range);

                                var rec = new Polygon.Trapezoid(startPosition, endPosition, input.Radius * 1.4f, input.EndRadius * 1.8f);

                                if (rec.IsOutside(output.TargetPosition.To2D()))
                                {
                                    continue;
                                }

                                polygons[rec] = targets.Where(x => rec.IsInside(x.TargetPosition)).ToList();
                            }
                        }

                        var polygon = polygons.MaxOrDefault(x => x.Value.Count);
                        if (polygon.Key != null)
                        {
                            var positions = polygon.Value.ToList();
                            var center = positions.Aggregate(new Vector3(), (sum, pos) => sum + pos.TargetPosition) / positions.Count;

                            if (positions.Count == 0)
                            {
                                output.HitChance = HitChance.Impossible;
                                return;
                            }

                            var max = positions.Max(
                                x => input.UseBlink
                                         ? output.TargetPosition.Distance(x.TargetPosition)
                                         : input.Caster.Distance(x.TargetPosition));
                            var range = Math.Min(input.UseBlink ? input.Range : input.CastRange, max);

                            output.CastPosition = input.UseBlink
                                                      ? output.TargetPosition.Extend2D(center, range)
                                                      : input.Caster.Position.Extend2D(center, range);
                            output.AoeTargetsHit = polygon.Value;
                        }
                    }
                    else
                    {
                        output.AoeTargetsHit.Add(output);

                        if (input.UseBlink)
                        {
                            input.AreaOfEffect = false;
                        }
                    }

                    if (input.UseBlink)
                    {
                        output.BlinkLinePosition = input.Caster.Distance(output.TargetPosition) > input.CastRange
                                                       ? input.Caster.Position.Extend2D(output.TargetPosition, input.CastRange)
                                                       : output.TargetPosition.Extend2D(output.CastPosition, -100);

                        if (input.Caster.Distance(output.BlinkLinePosition) > input.CastRange)
                        {
                            output.HitChance = HitChance.Impossible;
                        }
                    }

                    break;
                }

                case SkillShotType.Line:
                {
                    targets.Insert(0, output);

                    if (targets.Count > 1)
                    {
                        // yolo
                        var polygons = new Dictionary<Polygon.Rectangle, List<PredictionOutput9>>();

                        if (input.UseBlink)
                        {
                            var targetPosition = output.TargetPosition;
                            var otherTargets = targets.Skip(1).ToList();

                            foreach (var predictionOutput9 in otherTargets)
                            {
                                var aoeTargetPosition = predictionOutput9.TargetPosition;
                                var averagePosition = (targetPosition + aoeTargetPosition) / 2;
                                var start = targetPosition.Extend2D(aoeTargetPosition, -100);
                                var end = start.Extend2D(aoeTargetPosition, input.Range);
                                var rec = new Polygon.Rectangle(start, end, input.Radius);

                                foreach (var output9 in otherTargets)
                                {
                                    if (output9.Target == predictionOutput9.Target)
                                    {
                                        continue;
                                    }

                                    var averagePosition2 = (averagePosition + output9.TargetPosition) / 2;
                                    var start2 = targetPosition.Extend2D(averagePosition2, -100);
                                    var end2 = start2.Extend2D(averagePosition2, input.Range);
                                    var rec2 = new Polygon.Rectangle(start2, end2, input.Radius + 50);

                                    if (!rec2.IsInside(aoeTargetPosition) || !rec2.IsInside(output9.TargetPosition))
                                    {
                                        continue;
                                    }

                                    rec = rec2;
                                }

                                polygons[rec] = targets.Where(x => rec.IsInside(x.TargetPosition)).ToList();
                            }
                        }
                        else
                        {
                            var startPosition = input.Caster.Position;

                            foreach (var predictionOutput in targets)
                            {
                                var endPosition = startPosition.Extend2D(predictionOutput.TargetPosition, input.Range);

                                var rec = new Polygon.Rectangle(startPosition, endPosition, input.Radius * 1.3f);

                                if (rec.IsOutside(output.TargetPosition.To2D()))
                                {
                                    continue;
                                }

                                polygons[rec] = targets.Where(x => rec.IsInside(x.TargetPosition)).ToList();
                            }
                        }

                        var polygon = polygons.MaxOrDefault(x => x.Value.Count);
                        if (polygon.Key != null)
                        {
                            var positions = polygon.Value.ToList();
                            var center = positions.Aggregate(new Vector3(), (sum, pos) => sum + pos.TargetPosition) / positions.Count;

                            if (positions.Count == 0)
                            {
                                output.HitChance = HitChance.Impossible;
                                return;
                            }

                            var max = positions.Max(
                                x => input.UseBlink
                                         ? output.TargetPosition.Distance(x.TargetPosition)
                                         : input.Caster.Distance(x.TargetPosition));
                            var range = Math.Min(input.UseBlink ? input.Range : input.CastRange, max);

                            output.CastPosition = input.UseBlink
                                                      ? output.TargetPosition.Extend2D(center, range)
                                                      : input.Caster.Position.Extend2D(center, range);
                            output.AoeTargetsHit = polygon.Value;
                        }
                    }
                    else
                    {
                        output.AoeTargetsHit.Add(output);

                        if (input.UseBlink)
                        {
                            input.AreaOfEffect = false;
                        }
                    }

                    if (input.UseBlink)
                    {
                        output.BlinkLinePosition = input.Caster.Distance(output.TargetPosition) > input.CastRange
                                                       ? input.Caster.Position.Extend2D(output.TargetPosition, input.CastRange)
                                                       : output.TargetPosition.Extend2D(output.CastPosition, -100);

                        if (input.Caster.Distance(output.BlinkLinePosition) > input.CastRange)
                        {
                            output.HitChance = HitChance.Impossible;
                        }
                    }

                    break;
                }
            }
        }
    }
}