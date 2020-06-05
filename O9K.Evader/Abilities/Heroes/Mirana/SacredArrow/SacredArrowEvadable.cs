namespace O9K.Evader.Abilities.Heroes.Mirana.SacredArrow
{
    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.Mirana;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearProjectile;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class SacredArrowEvadable : LinearProjectileEvadable, IUnit, IModifierCounter
    {
        private readonly SacredArrow sacredArrow;

        private SacredArrowObstacle arrow1;

        private SacredArrowObstacle arrow2;

        private SacredArrowObstacle arrow3;

        public SacredArrowEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.sacredArrow = (SacredArrow)ability;

            this.Counters.Add(Abilities.BallLightning);
            // Counters.Add(Abilities.MantaStyle); //todo test
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.Add(Abilities.Bulwark);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.Add(Abilities.PressTheAttack);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddUnit(Unit unit)
        {
            if (!this.Owner.IsVisible)
            {
                var time = Game.RawGameTime - (Game.Ping / 2000);

                var obstacle = new LinearProjectileUnitObstacle(this, unit)
                {
                    EndCastTime = time,
                    EndObstacleTime = time + (this.RangedAbility.Range / this.RangedAbility.Speed)
                };

                this.Pathfinder.AddObstacle(obstacle);
                return;
            }

            this.arrow1?.AddUnit(unit);
            this.arrow2?.AddUnit(unit);
            this.arrow3?.AddUnit(unit);
        }

        public override void PhaseCancel()
        {
            this.EndCastTime = 0;
            this.StartCastTime = 0;

            this.Pathfinder.CancelObstacle(this.Ability.Handle);

            if (this.sacredArrow.MultipleArrows)
            {
                this.Pathfinder.CancelObstacle(this.Ability.Handle);
                this.Pathfinder.CancelObstacle(this.Ability.Handle);
            }
        }

        protected override void AddObstacle()
        {
            this.arrow1 = new SacredArrowObstacle(this, this.Ability.Owner.Position, 0)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + (this.RangedAbility.Range / this.RangedAbility.Speed)
            };

            this.Pathfinder.AddObstacle(this.arrow1);

            if (!this.sacredArrow.MultipleArrows)
            {
                return;
            }

            this.arrow2 = new SacredArrowObstacle(this, this.Ability.Owner.Position, 25)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + (this.RangedAbility.Range / this.RangedAbility.Speed)
            };

            this.Pathfinder.AddObstacle(this.arrow2);

            this.arrow3 = new SacredArrowObstacle(this, this.Ability.Owner.Position, -24)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + (this.RangedAbility.Range / this.RangedAbility.Speed)
            };

            this.Pathfinder.AddObstacle(this.arrow3);
        }
    }
}