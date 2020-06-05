namespace O9K.AIO.Heroes.Timbersaw.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using SharpDX;

    using TargetManager;

    using BaseTimberChain = Core.Entities.Abilities.Heroes.Timbersaw.TimberChain;

    internal class TimberChainBlink : BlinkAbility
    {
        private readonly BaseTimberChain timberChain;

        public TimberChainBlink(ActiveAbility ability)
            : base(ability)
        {
            this.timberChain = (BaseTimberChain)ability;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
        {
            var position = this.GetClosestTree(toPosition);
            if (position.IsZero)
            {
                return false;
            }

            if (!this.Ability.UseAbility(this.Owner.Position.Extend2D(position, 400)))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(position);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }

        private Vector3 GetClosestTree(Vector3 preferedPosition)
        {
            var ownerPosition = this.Owner.Position;
            var maxPosition = ownerPosition.Extend2D(preferedPosition, this.Ability.CastRange);
            var availableTress = EntityManager9.Trees.Where(x => x.Distance2D(ownerPosition) < this.Ability.CastRange)
                .OrderBy(x => x.Distance2D(preferedPosition))
                .ToArray();

            foreach (var tree in availableTress)
            {
                var treePosition = tree.Position;
                if (ownerPosition.Distance2D(treePosition) < 500)
                {
                    continue;
                }

                var angle = (tree.Position - ownerPosition).AngleBetween(maxPosition - tree.Position);

                if (angle > 60)
                {
                    continue;
                }

                var polygon = new Polygon.Rectangle(ownerPosition, tree.Position, this.timberChain.ChainRadius);

                if (availableTress.Any(x => x != tree && polygon.IsInside(x.Position)))
                {
                    continue;
                }

                return tree.Position;
            }

            return Vector3.Zero;
        }
    }
}