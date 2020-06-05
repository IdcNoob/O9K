namespace O9K.Evader.Abilities.Heroes.MonkeyKing.TreeDance
{
    using System;
    using System.Linq;

    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Logger;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Metadata;

    using Pathfinder.Obstacles;

    using SharpDX;

    internal class TreeDanceUsable : BlinkAbility, IDisposable
    {
        private readonly Tree[] trees;

        private Tree tree;

        private ActiveAbility ult;

        private float ultEndTime;

        private Vector3 ultPosition;

        public TreeDanceUsable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.trees = ObjectManager.GetEntities<Tree>().ToArray();
            Player.OnExecuteOrder += this.OnExecuteOrder;
        }

        private ActiveAbility Ult
        {
            get
            {
                if (this.ult?.IsValid != true)
                {
                    this.ult = (ActiveAbility)this.Owner.Abilities.FirstOrDefault(x => x.Id == AbilityId.monkey_king_wukongs_command);
                }

                return this.ult;
            }
        }

        public void Dispose()
        {
            Player.OnExecuteOrder -= this.OnExecuteOrder;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var remainingTime = obstacle.GetEvadeTime(ally, true) - this.ActiveAbility.CastPoint;

            this.tree = this.trees
                .Where(
                    x => x.IsValid && x.IsAlive && ally.Distance(x.Position) < this.ActiveAbility.CastRange
                         && (Game.RawGameTime > this.ultEndTime || x.Distance2D(this.ultPosition) < this.ult.Radius - 50))
                .OrderBy(x => x.Distance2D(this.FountainPosition))
                .FirstOrDefault(x => ally.GetTurnTime(x.Position) + 0.1f < remainingTime);

            if (this.tree == null)
            {
                return 9999;
            }

            return this.ActiveAbility.GetCastDelay(this.tree.Position) + 0.15f;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.UseAbility(this.tree, false, true);
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!args.Process || args.IsQueued || args.OrderId != OrderId.AbilityLocation)
                {
                    return;
                }

                if (args.Ability.Handle != this.Ult?.Handle)
                {
                    return;
                }

                this.ultEndTime = Game.RawGameTime + this.ult.Duration;
                this.ultPosition = args.TargetPosition;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}