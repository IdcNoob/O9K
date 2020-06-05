namespace O9K.AIO.Heroes.FacelessVoid.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class TimeDilation : DebuffAbility
    {
        public TimeDilation(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (targetManager.Target.Abilities.Count(x => x.RemainingCooldown > 0) >= 2)
            {
                return true;
            }

            var allAbilities = targetManager.EnemyHeroes.SelectMany(x => x.Abilities).Count(x => x.RemainingCooldown > 0);
            if (allAbilities >= 5)
            {
                return true;
            }

            return false;
        }
    }
}