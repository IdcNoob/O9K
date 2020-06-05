namespace O9K.AIO.Heroes.Dynamic.Abilities
{
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Units;
    using Core.Helpers;

    internal abstract class OldUsableAbility
    {
        protected OldUsableAbility(IActiveAbility ability)
        {
            this.Ability = ability;
            this.Owner = this.Ability.Owner;
        }

        public IActiveAbility Ability { get; }

        public MultiSleeper AbilitySleeper { get; set; }

        public MultiSleeper OrbwalkSleeper { get; set; }

        public Unit9 Owner { get; }

        public MultiSleeper TargetSleeper { get; set; } = new MultiSleeper();

        public virtual bool CanBeCasted(ComboModeMenu menu)
        {
            if (!menu.IsAbilityEnabled(this.Ability))
            {
                return false;
            }

            if (this.AbilitySleeper.IsSleeping(this.Ability.Handle) || this.OrbwalkSleeper.IsSleeping(this.Ability.Owner.Handle))
            {
                return false;
            }

            if (!this.Ability.CanBeCasted())
            {
                return false;
            }

            return true;
        }

        public virtual bool CanBeCasted(Unit9 target, ComboModeMenu menu, bool canHitCheck = true)
        {
            if (!this.CanBeCasted(menu))
            {
                return false;
            }

            if (canHitCheck && !this.CanHit(target))
            {
                return false;
            }

            if (!this.ShouldCast(target))
            {
                return false;
            }

            return true;
        }

        public virtual bool CanHit(Unit9 target)
        {
            return this.Ability.CanHit(target);
        }

        public abstract bool ShouldCast(Unit9 target);

        public abstract bool Use(Unit9 target);
    }
}