namespace O9K.AIO.Heroes.EmberSpirit.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    internal class SearingChains : DisableAbility
    {
        public SearingChains(ActiveAbility ability)
            : base(ability)
        {
        }

        protected override bool ChainStun(Unit9 target, bool invulnerability)
        {
            if (!invulnerability)
            {
                return this.Owner.HasModifier("modifier_ember_spirit_sleight_of_fist_caster");
            }

            var immobile = target.GetInvulnerabilityDuration();
            if (immobile <= 0)
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(target);
            if (target.IsInvulnerable)
            {
                hitTime -= 0.1f;
            }

            return hitTime > immobile;
        }
    }
}