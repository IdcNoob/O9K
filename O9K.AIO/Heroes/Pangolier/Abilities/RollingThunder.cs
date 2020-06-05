namespace O9K.AIO.Heroes.Pangolier.Abilities
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    using BaseRollingThunder = Core.Entities.Abilities.Heroes.Pangolier.RollingThunder;

    internal class RollingThunder : NukeAbility
    {
        private readonly NavMeshPathfinding navMesh = new NavMeshPathfinding();

        private readonly BaseRollingThunder rollingThunder;

        public RollingThunder(ActiveAbility ability)
            : base(ability)
        {
            this.rollingThunder = (BaseRollingThunder)ability;
        }

        public Vector3 GetPosition(Unit9 target)
        {
            var wallPositions = new List<Vector3>();

            var center = this.Owner.Position;
            const int CellCount = 40;
            for (var i = 0; i < CellCount; ++i)
            {
                for (var j = 0; j < CellCount; ++j)
                {
                    var p = new Vector2(
                        (this.navMesh.CellSize * (i - (CellCount / 2))) + center.X,
                        (this.navMesh.CellSize * (j - (CellCount / 2))) + center.Y);

                    if ((this.navMesh.GetCellFlags(p) & NavMeshCellFlags.InteractionBlocker) != 0)
                    {
                        wallPositions.Add(new Vector3(p.X, p.Y, 0));
                    }
                }
            }

            return wallPositions.Where(this.CheckWall).OrderBy(x => this.Owner.Distance(x)).FirstOrDefault();
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (this.Owner.GetAngle(targetManager.Target.Position) > 0.75f)
            {
                return false;
            }

            return true;
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var blink = usableAbilities.Find(x => x.Ability.Id == AbilityId.item_blink);
            if (blink == null)
            {
                return this.Owner.Distance(targetManager.Target) < 600;
            }

            return this.Owner.Distance(targetManager.Target) < blink.Ability.CastRange + 200;
        }

        private bool CheckWall(Vector3 wall)
        {
            var distance = this.Owner.Distance(wall);
            // var turnTime = (this.rollingThunder.TurnRate * this.Owner.GetAngle(wall))- 0.75f;
            var turnTime = (this.Owner.GetAngle(wall) / this.rollingThunder.TurnRate) + 0.5f;

            if (turnTime > distance / this.rollingThunder.Speed)
            {
                return false;
            }

            if (distance > 600)
            {
                return false;
            }

            return true;
        }
    }
}