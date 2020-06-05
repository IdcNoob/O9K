namespace O9K.AIO.Heroes.Tiny.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Managers.Entity;

    using Ensage.SDK.Extensions;

    using TargetManager;

    internal class TreeGrab : UsableAbility
    {
        public TreeGrab(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            return false;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var ownerPosition = this.Owner.Position;
            var tree = EntityManager9.Trees.Where(x => x.Distance2D(ownerPosition) < this.Ability.CastRange)
                .OrderBy(x => this.Owner.GetAngle(x.Position))
                .FirstOrDefault();

            if (tree == null)
            {
                return false;
            }

            if (!this.Ability.UseAbility(tree))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}