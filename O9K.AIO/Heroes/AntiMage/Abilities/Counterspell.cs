namespace O9K.AIO.Heroes.AntiMage.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Entity;

    using TargetManager;

    internal class Counterspell : ShieldAbility
    {
        public Counterspell(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (!EntityManager9.Abilities.OfType<ActiveAbility>()
                    .Any(
                        x => x.UnitTargetCast && x.TargetsEnemy && x.CastPoint <= 0.1f && !x.Owner.IsAlly(this.Owner)
                             && x.CanHit(this.Owner) && x.CanBeCasted() && x.Owner.GetAngle(this.Owner, true) < 0.5f))
            {
                return false;
            }

            return true;
        }
    }
}