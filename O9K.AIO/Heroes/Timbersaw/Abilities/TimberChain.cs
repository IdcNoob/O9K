namespace O9K.AIO.Heroes.Timbersaw.Abilities
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    using BaseTimberChain = Core.Entities.Abilities.Heroes.Timbersaw.TimberChain;

    internal class TimberChain : NukeAbility
    {
        private readonly BaseTimberChain timberChain;

        private Vector3 castPosition;

        public TimberChain(ActiveAbility ability)
            : base(ability)
        {
            this.timberChain = (BaseTimberChain)ability;
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var target = targetManager.Target;
            var input = this.Ability.GetPredictionInput(target);
            input.Delay += this.Owner.Distance(target) / this.Ability.Speed;
            var output = this.Ability.GetPredictionOutput(input);
            var ownerPosition = this.Owner.Position;

            this.castPosition = output.CastPosition;

            var polygon = new Polygon.Rectangle(ownerPosition, this.castPosition, this.timberChain.ChainRadius);
            var availableTrees = EntityManager9.Trees.Where(x => x.Distance2D(ownerPosition) < this.Ability.CastRange).ToArray();

            foreach (var tree in availableTrees)
            {
                if (polygon.IsInside(tree.Position))
                {
                    return false;
                }
            }

            var chainEndPosition = ownerPosition.Extend2D(this.castPosition, this.Ability.CastRange);
            polygon = new Polygon.Rectangle(this.castPosition, chainEndPosition, this.timberChain.Radius);

            foreach (var tree in availableTrees)
            {
                if (polygon.IsInside(tree.Position))
                {
                    this.castPosition = tree.Position;
                    return true;
                }
            }

            if (this.Ability.Level < 4 || ownerPosition.Distance2D(this.castPosition) < 400)
            {
                return false;
            }

            foreach (var tree in availableTrees.OrderBy(x => x.Distance2D(this.castPosition)))
            {
                var treePosition = tree.Position;

                if (treePosition.Distance2D(this.castPosition) > 500 && target.GetAngle(treePosition) > 0.75f)
                {
                    continue;
                }

                if (ownerPosition.Distance2D(this.castPosition) < treePosition.Distance2D(this.castPosition))
                {
                    continue;
                }

                polygon = new Polygon.Rectangle(ownerPosition, treePosition, this.timberChain.ChainRadius);
                if (availableTrees.Any(x => !x.Equals(tree) && polygon.IsInside(x.Position)))
                {
                    continue;
                }

                this.castPosition = treePosition;
                return true;
            }

            return false;
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var blink = usableAbilities.Find(x => x.Ability.Id == AbilityId.item_blink);
            if (blink == null)
            {
                return true;
            }

            if (this.Owner.Distance(targetManager.Target) < 800)
            {
                return true;
            }

            return false;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(this.Owner.Position.Extend2D(this.castPosition, 400)))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(this.castPosition);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(this.Ability.GetHitTime(this.castPosition));
            return true;
        }
    }
}