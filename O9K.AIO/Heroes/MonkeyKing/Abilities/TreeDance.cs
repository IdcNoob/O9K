namespace O9K.AIO.Heroes.MonkeyKing.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Managers.Entity;

    using Ensage.SDK.Extensions;

    using SharpDX;

    using TargetManager;

    internal class TreeDance : BlinkAbility
    {
        public TreeDance(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (targetManager.Target == null)
            {
                return true;
            }

            if (this.Owner.Distance(targetManager.Target) < 500)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var targetPosition = target.Position;
            var ownerPosition = this.Owner.Position;

            var tree = EntityManager9.Trees.Where(x => x.Distance2D(ownerPosition) < this.Ability.CastRange)
                .OrderBy(x => x.Distance2D(targetPosition))
                .FirstOrDefault(x => x.Distance2D(targetPosition) > 300 && x.Distance2D(targetPosition) < 800);

            if (tree == null)
            {
                return false;
            }

            if (!this.Ability.UseAbility(tree))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(tree.Position);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, float minDistance, float blinkDistance)
        {
            if (this.Owner.Distance(targetManager.Target) < minDistance)
            {
                return false;
            }

            var target = targetManager.Target;
            var targetPosition = target.Position;
            var ownerPosition = this.Owner.Position;

            var tree = EntityManager9.Trees.Where(x => x.Distance2D(ownerPosition) < this.Ability.CastRange)
                .OrderBy(x => x.Distance2D(targetPosition))
                .FirstOrDefault(x => x.Distance2D(targetPosition) < 400);

            if (tree == null)
            {
                return false;
            }

            if (!this.Ability.UseAbility(tree))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(tree.Position);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
        {
            if (this.Owner.Distance(toPosition) < 300)
            {
                return false;
            }

            var ownerPosition = this.Owner.Position;

            var tree = EntityManager9.Trees.Where(x => x.Distance2D(ownerPosition) < this.Ability.CastRange)
                .OrderBy(x => x.Distance2D(toPosition))
                .FirstOrDefault(x => x.Distance2D(toPosition) < 600);

            if (tree == null)
            {
                return false;
            }

            if (!this.Ability.UseAbility(tree))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(tree.Position);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }
    }
}