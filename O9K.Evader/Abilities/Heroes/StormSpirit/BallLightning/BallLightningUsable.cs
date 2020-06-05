namespace O9K.Evader.Abilities.Heroes.StormSpirit.BallLightning
{
    using System.Linq;

    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;

    using Grimstroke.PhantomsEmbrace;

    using Gyrocopter.HomingMissile;

    using Metadata;

    using Pathfinder.Obstacles;

    using SharpDX;

    using SpiritBreaker.ChargeOfDarkness;

    internal class BallLightningUsable : CounterAbility
    {
        private readonly IPathfinder pathfinder;

        private Vector3 position;

        public BallLightningUsable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, menu)
        {
            this.pathfinder = pathfinder;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var additionalTime = 0f;
            this.position = Vector3.Zero;

            switch (obstacle.EvadableAbility)
            {
                case HomingMissileEvadable _:
                {
                    var missilePosition = ((HomingMissileObstacle)obstacle).Position;
                    this.position = ally.Position.Extend2D(missilePosition, 199);
                    additionalTime += ally.GetTurnTime(this.position);
                    break;
                }
                case PhantomsEmbraceEvadable _:
                {
                    var casterPosition = obstacle.Caster.Position;
                    var range = 225 + (50 * this.Ability.Level);
                    this.position = ally.Position.Extend2D(casterPosition, range);
                    additionalTime += ally.GetTurnTime(this.position) + 0.07f;
                    break;
                }
                case ChargeOfDarknessEvadable _:
                {
                    var spiritBreakerPosition = obstacle.Caster.Position;
                    var range = 175 + (50 * this.Ability.Level);
                    this.position = ally.Position.Extend2D(spiritBreakerPosition, range);
                    additionalTime += ally.GetTurnTime(this.position) + 0.3f;
                    break;
                }
                case TargetableProjectileEvadable projectile:
                {
                    if (!projectile.IsDisjointable)
                    {
                        this.position = ally.Position.Extend2D(enemy.Position, 199);
                        additionalTime += ally.GetTurnTime(this.position);
                    }

                    if (obstacle.EvadableAbility.Ability.Id == AbilityId.sven_storm_bolt)
                    {
                        this.position = ally.InFront(300);
                    }

                    additionalTime += 0.05f;
                    break;
                }
                case LinearProjectileEvadable _:
                case ConeProjectileEvadable _:
                case AreaOfEffectEvadable _ when obstacle.EvadableAbility.Ability.Id == AbilityId.venomancer_poison_nova:
                {
                    var path = this.pathfinder.GetPathFromObstacle(ally, ally.Speed, ally.Position, 69, out var success);
                    if (!success)
                    {
                        return 69;
                    }

                    this.position = path.Last();
                    additionalTime += ally.GetTurnTime(this.position) + 0.05f;
                    break;
                }
                case AreaOfEffectEvadable _:
                {
                    //todo more aoe spells with speed

                    var pathAoe = this.pathfinder.GetPathFromObstacle(ally, ally.Speed, ally.Position, 69, out var success);
                    if (!success)
                    {
                        return 69;
                    }

                    var safePosition = pathAoe.Last().Extend2D(this.Owner.Position, -50);

                    switch (obstacle.EvadableAbility.Ability.Id)
                    {
                        case AbilityId.nevermore_requiem:
                        {
                            if (ally.Distance(enemy.Position) < 600)
                            {
                                this.position = safePosition;
                            }
                            else
                            {
                                var jumpPosition = ally.Position.Extend2D(enemy.Position, 600);
                                this.position = this.Owner.Distance(jumpPosition) > this.Owner.Distance(safePosition)
                                                    ? safePosition
                                                    : jumpPosition;
                            }

                            additionalTime += ally.GetTurnTime(this.position) + 0.15f;
                            break;
                        }
                        case AbilityId.tidehunter_ravage:
                        {
                            if (ally.Distance(enemy.Position) < 600)
                            {
                                this.position = safePosition;
                            }
                            else
                            {
                                var jumpPosition = ally.Position.Extend2D(enemy.Position, 600);
                                this.position = this.Owner.Distance(jumpPosition) > this.Owner.Distance(safePosition)
                                                    ? safePosition
                                                    : jumpPosition;
                            }

                            additionalTime += ally.GetTurnTime(this.position) + 0.1f;
                            break;
                        }
                    }

                    break;
                }
            }

            if (this.position.IsZero)
            {
                this.position = ally.InFront(ally.IsMoving ? 190 : 90);
            }

            return this.ActiveAbility.GetHitTime(ally) + additionalTime;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(this.position);
            return this.ActiveAbility.UseAbility(this.position, false, true);
        }
    }
}