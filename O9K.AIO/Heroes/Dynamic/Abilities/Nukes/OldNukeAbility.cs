namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    internal class OldNukeAbility : OldUsableAbility
    {
        public OldNukeAbility(INuke ability)
            : base(ability)
        {
            this.Nuke = ability;
        }

        public INuke Nuke { get; }

        public override bool ShouldCast(Unit9 target)
        {
            if (this.Ability.UnitTargetCast && !target.IsVisible)
            {
                return false;
            }

            if (target.IsReflectingDamage)
            {
                return false;
            }

            if (this.Ability.BreaksLinkens && target.IsBlockingAbilities)
            {
                return false;
            }

            var damage = this.Nuke.GetDamage(target);
            if (damage <= 0)
            {
                return false;
            }

            if ((target.IsStunned || target.IsHexed) && this.Ability is IDisable && damage < target.Health)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                if (this.Nuke.UnitTargetCast)
                {
                    return false;
                }

                var immobile = target.GetImmobilityDuration();
                if (immobile <= 0 || immobile + 0.05f > this.Nuke.GetHitTime(target))
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