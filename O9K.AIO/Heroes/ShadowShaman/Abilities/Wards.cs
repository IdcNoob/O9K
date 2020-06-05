namespace O9K.AIO.Heroes.ShadowShaman.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using TargetManager;

    internal class Wards : AoeAbility
    {
        public Wards(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (!target.IsVisible)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                if (!this.ChainStun(target, true))
                {
                    return false;
                }
            }

            if ((target.IsRooted || target.IsStunned || target.IsHexed) && target.GetImmobilityDuration() <= 0)
            {
                return false;
            }

            return true;
        }

        protected override bool ChainStun(Unit9 target, bool invulnerability)
        {
            var immobile = invulnerability ? target.GetInvulnerabilityDuration() : target.GetImmobilityDuration();
            if (immobile <= 0)
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(target) + 0.5f;
            if (target.IsInvulnerable)
            {
                hitTime -= 0.1f;
            }

            return hitTime > immobile;
        }
    }
}