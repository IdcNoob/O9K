namespace O9K.Evader.Abilities.Heroes.Abaddon.AphoticShield
{
    using System;

    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.Abaddon;
    using Core.Entities.Units;
    using Core.Extensions;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class AphoticShieldUsable : CounterAbility
    {
        private readonly IActionManager actionManager;

        private readonly AphoticShield aphoticShield;

        public AphoticShieldUsable(Ability9 ability, IActionManager actionManager, IMainMenu menu)
            : base(ability, menu)
        {
            this.aphoticShield = (AphoticShield)ability;
            this.actionManager = actionManager;
            this.ModifierName = null;
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            if (this.aphoticShield.IsCasting)
            {
                return false;
            }

            if (obstacle.IsModifierObstacle)
            {
                return true;
            }

            var damage = obstacle.GetDamage(ally);
            if (damage >= ally.Health + this.aphoticShield.BlockValue(ally))
            {
                return false;
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var requiredTime = base.GetRequiredTime(ally, enemy, obstacle);
            var ability = obstacle.EvadableAbility.Ability;
            if (obstacle.IsModifierObstacle || obstacle.GetDamage(ally) > ally.Health)
            {
                return requiredTime;
            }

            if (ability.IsDisable() && !this.Owner.Equals(ally))
            {
                var remainingTime = obstacle.GetEvadeTime(ally, false);
                // todo check if ignores modifier even when not used
                this.actionManager.IgnoreModifierObstacle(ability.Handle, ally, remainingTime + 0.5f);
                return Math.Min(requiredTime - 0.15f, remainingTime - 0.03f);
            }

            return requiredTime;
        }
    }
}