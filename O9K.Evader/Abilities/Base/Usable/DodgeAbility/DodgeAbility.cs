namespace O9K.Evader.Abilities.Base.Usable.DodgeAbility
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;

    using Evadable;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class DodgeAbility : UsableAbility
    {
        public DodgeAbility(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            this.SpeedBuffAbility = (ISpeedBuff)ability;
            this.ModifierName = this.SpeedBuffAbility.BuffModifierName;
        }

        public ISpeedBuff SpeedBuffAbility { get; }

        protected string ModifierName { get; }

        public override void AddEvadableAbility(EvadableAbility evadable)
        {
            // ignored
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!this.Menu.AbilitySettings.IsDodgeEnabled(this.Ability.Name))
            {
                return false;
            }

            if (!ally.Equals(this.Owner))
            {
                return false;
            }

            if (!this.Ability.CanBeCasted(false))
            {
                return false;
            }

            if (!this.ActiveAbility.CanHit(ally))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(this.ModifierName) && ally.HasModifier(this.ModifierName))
            {
                return false;
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.GetHitTime(ally);
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.UseAbility(ally, false, true);
        }
    }
}