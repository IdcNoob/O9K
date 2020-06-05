namespace O9K.AIO.Heroes.Mars.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class SpearOfMars : NukeAbility
    {
        private readonly NavMeshPathfinding navMesh = new NavMeshPathfinding();

        private Vector3 castPosition;

        public SpearOfMars(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var target = targetManager.Target;

            if (target.IsMagicImmune && !this.Ability.CanHitSpellImmuneEnemy)
            {
                return false;
            }

            if (this.Owner.Distance(target) > this.Ability.CastRange)
            {
                return false;
            }

            // ult
            if (target.HasModifier("modifier_mars_arena_of_blood_leash"))
            {
                return true;
            }

            var mainInput = this.Ability.GetPredictionInput(target);
            mainInput.Range = this.Ability.CastRange;
            var mainOutput = this.Ability.PredictionManager.GetSimplePrediction(mainInput);
            var targetPredictedPosition = mainOutput.TargetPosition;
            var range = this.Ability.Range - 200;
            var width = this.Ability.Radius;

            //todo predict collision positions ?
            var collisionRec = new Polygon.Rectangle(this.Owner.Position, targetPredictedPosition, width);
            if (targetManager.AllEnemyHeroes.Any(x => x != target && collisionRec.IsInside(x.Position)))
            {
                return false;
            }

            // tree
            foreach (var tree in EntityManager9.Trees)
            {
                var rec = new Polygon.Rectangle(
                    targetPredictedPosition,
                    this.Owner.Position.Extend2D(targetPredictedPosition, range),
                    width - 50);

                if (rec.IsInside(tree.Position))
                {
                    this.castPosition = mainOutput.CastPosition;
                    return true;
                }
            }

            // building
            foreach (var building in EntityManager9.Units.Where(x => x.IsBuilding && x.IsAlive))
            {
                var rec = new Polygon.Rectangle(
                    targetPredictedPosition,
                    this.Owner.Position.Extend2D(targetPredictedPosition, range),
                    width);

                if (rec.IsInside(building.Position))
                {
                    this.castPosition = mainOutput.CastPosition;
                    return true;
                }
            }

            // wall
            const int CellCount = 30;
            for (var i = 0; i < CellCount; ++i)
            {
                for (var j = 0; j < CellCount; ++j)
                {
                    var p = new Vector2(
                        (this.navMesh.CellSize * (i - (CellCount / 2))) + targetPredictedPosition.X,
                        (this.navMesh.CellSize * (j - (CellCount / 2))) + targetPredictedPosition.Y);

                    if ((this.navMesh.GetCellFlags(p) & NavMeshCellFlags.InteractionBlocker) != 0)
                    {
                        var rec = new Polygon.Rectangle(
                            targetPredictedPosition,
                            this.Owner.Position.Extend2D(targetPredictedPosition, range),
                            width - 100);

                        if (rec.IsInside(p))
                        {
                            this.castPosition = mainOutput.CastPosition;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (this.castPosition.IsZero)
            {
                return base.UseAbility(targetManager, comboSleeper, aoe);
            }

            if (!this.Ability.UseAbility(this.castPosition))
            {
                return false;
            }

            this.castPosition = Vector3.Zero;

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            if (this.Ability is IDisable disable)
            {
                targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, this.Ability.GetHitTime(targetManager.Target));
            }

            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}