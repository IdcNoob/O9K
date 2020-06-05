namespace O9K.AIO.Heroes.Riki.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    internal class SmokeScreen : DisableAbility
    {
        public SmokeScreen(ActiveAbility ability)
            : base(ability)
        {
        }

        protected override bool ChainStun(Unit9 target, bool invulnerability)
        {
            var immobile = target.GetInvulnerabilityDuration();
            if (immobile > 0)
            {
                var hitTime = this.Ability.GetHitTime(target);
                if (target.IsInvulnerable)
                {
                    hitTime -= 0.1f;
                }

                return hitTime > immobile;
            }

            return true;
        }
    }
}