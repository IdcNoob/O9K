namespace O9K.AIO.Heroes.Dynamic.Abilities.Buffs
{
    using System.Linq;

    using AIO.Modes.Combo;

    using Blinks;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    internal class OldBuffAbility : OldUsableAbility
    {
        public OldBuffAbility(IBuff ability)
            : base(ability)
        {
            this.Buff = ability;
        }

        public IBuff Buff { get; }

        public bool CanBeCasted(Unit9 target, Unit9 enemy, ComboModeMenu menu, BlinkAbilityGroup blinks)
        {
            if (!this.CanBeCasted(menu))
            {
                return false;
            }

            if (!this.CanHit(target))
            {
                return false;
            }

            if (!this.ShouldCast(target))
            {
                return false;
            }

            if (!this.ShouldCastBuff(enemy, blinks, menu))
            {
                return false;
            }

            return true;
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (target.IsInvulnerable)
            {
                return false;
            }

            if (target.Equals(this.Buff.Owner))
            {
                if (!this.Buff.BuffsOwner)
                {
                    return false;
                }
            }
            else
            {
                if (!this.Buff.BuffsAlly)
                {
                    return false;
                }
            }

            if (target.IsMagicImmune && !this.Buff.PiercesMagicImmunity(target))
            {
                return false;
            }

            if (target.HasModifier(this.Buff.BuffModifierName))
            {
                return false;
            }

            if (this.Buff is ToggleAbility toggle && toggle.Enabled)
            {
                return false;
            }

            return true;
        }

        public override bool Use(Unit9 target)
        {
            if (!this.Ability.UseAbility(target, EntityManager9.AllyHeroes, HitChance.Low))
            {
                return false;
            }

            this.OrbwalkSleeper.Sleep(this.Ability.Owner.Handle, this.Ability.GetCastDelay(target));
            this.AbilitySleeper.Sleep(this.Ability.Handle, this.Ability.GetHitTime(target) + 0.5f);

            return true;
        }

        protected virtual bool ShouldCastBuff(Unit9 target, BlinkAbilityGroup blinks, ComboModeMenu menu)
        {
            var distance = this.Ability.Owner.Distance(target);
            var attackRange = this.Ability.Owner.GetAttackRange(target);

            if (blinks.GetBlinkAbilities(this.Ability.Owner, menu).Any(x => x.Blink.Range > distance - attackRange))
            {
                return true;
            }

            if (distance > attackRange + 125)
            {
                return false;
            }

            return true;
        }
    }
}