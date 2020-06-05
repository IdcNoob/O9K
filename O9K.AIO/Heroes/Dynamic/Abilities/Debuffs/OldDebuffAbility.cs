namespace O9K.AIO.Heroes.Dynamic.Abilities.Debuffs
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    internal class OldDebuffAbility : OldUsableAbility
    {
        public OldDebuffAbility(IDebuff ability)
            : base(ability)
        {
            this.Debuff = ability;
        }

        public IDebuff Debuff { get; }

        public override bool ShouldCast(Unit9 target)
        {
            if (this.Ability.UnitTargetCast && !target.IsVisible)
            {
                return false;
            }

            if (target.HasModifier(this.Debuff.DebuffModifierName) && !(this.Debuff is INuke))
            {
                return false;
            }

            if (this.Ability.BreaksLinkens && target.IsBlockingAbilities)
            {
                return false;
            }

            if (target.IsDarkPactProtected)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                if (this.Debuff.UnitTargetCast)
                {
                    return false;
                }

                var immobile = target.GetImmobilityDuration();
                if (immobile <= 0 || immobile + 0.05f > this.Debuff.GetHitTime(target))
                {
                    return false;
                }
            }

            if (target.IsRooted && !this.Ability.UnitTargetCast && target.GetImmobilityDuration() <= 0)
            {
                return false;
            }

            return true;
        }

        public override bool Use(Unit9 target)
        {
            if (!this.Ability.UseAbility(target, EntityManager9.EnemyHeroes, HitChance.Low))
            {
                return false;
            }

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetCastDelay(target));
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(target) + 0.5f);

            return true;
        }
    }
}