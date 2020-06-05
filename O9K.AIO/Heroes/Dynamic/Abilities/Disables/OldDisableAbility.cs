namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables
{
    using System.Collections.Generic;

    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using Ensage;

    using Specials;

    internal class OldDisableAbility : OldUsableAbility
    {
        private readonly bool isAmplifier;

        public OldDisableAbility(IDisable ability)
            : base(ability)
        {
            this.Disable = ability;
            this.isAmplifier = ability is IHasDamageAmplify || ability.Id == AbilityId.item_bloodthorn
                                                            || ability.Id == AbilityId.item_orchid;
        }

        public IDisable Disable { get; }

        public virtual bool CanBeCasted(
            Unit9 target,
            List<OldDisableAbility> disables,
            List<OldSpecialAbility> specials,
            ComboModeMenu menu)
        {
            return this.CanBeCasted(target, menu);
        }

        public void SetTimings(Unit9 target)
        {
            var hitTime = this.Ability.GetHitTime(target) + 0.5f;

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetCastDelay(target) + 0.1f);
            this.AbilitySleeper.Sleep(this.Ability.Handle, hitTime + 0.1f);
            this.TargetSleeper.Sleep(target.Handle, 0.05f);
            target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (this.Ability.UnitTargetCast && !target.IsVisible)
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
                if (this.Disable.UnitTargetCast)
                {
                    return false;
                }

                var immobile = target.GetImmobilityDuration();
                if (immobile <= 0 || immobile + 0.05f > this.Disable.GetHitTime(target))
                {
                    return false;
                }
            }

            if (target.IsStunned)
            {
                return this.ChainStun(target);
            }

            if (target.IsHexed)
            {
                return (this.isAmplifier && !this.Disable.IsHex()) || this.ChainStun(target);
            }

            if (target.IsSilenced)
            {
                return !this.Disable.IsSilence(false) || this.isAmplifier || this.ChainStun(target);
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

            this.SetTimings(target);

            return true;
        }

        protected virtual bool ChainStun(Unit9 target)
        {
            if (this.Disable is INuke nuke && nuke.GetDamage(target) > target.Health)
            {
                return true;
            }

            var immobile = target.GetImmobilityDuration();
            if (immobile <= 0)
            {
                return false;
            }

            return this.Disable.GetHitTime(target) + 0.05f > immobile;
        }
    }
}