namespace O9K.AIO.Heroes.Tiny.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Entity;

    using TargetManager;

    internal class TreeVolley : AoeAbility
    {
        public TreeVolley(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (EntityManager9.Trees.Count(x => this.Owner.Distance(x.Position) < 525) < 3)
            {
                return false;
            }

            return true;
        }
    }
}