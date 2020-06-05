namespace O9K.AIO.Heroes.Juggernaut.Abililities
{
    using System.Linq;

    using Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Entity;

    using TargetManager;

    internal class Omnislash : NukeAbility
    {
        public Omnislash(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            var mod = this.Owner.GetModifier("modifier_juggernaut_omnislash");
            if (mod != null && mod.RemainingTime > this.Ability.CastPoint)
            {
                return false;
            }

            var target = targetManager.Target;
            var otherTargets = EntityManager9.Units.Where(
                    x => x.IsUnit && !x.Equals(target) && x.IsAlive && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(this.Owner)
                         && x.Distance(target) < this.Ability.Radius + 50)
                .ToList();

            if (otherTargets.Count(x => !x.IsHero || x.IsIllusion) > this.Ability.Level)
            {
                return false;
            }

            return true;
        }
    }
}