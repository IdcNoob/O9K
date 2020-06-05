namespace O9K.Evader.Abilities.Heroes.Slark.DarkPact
{
    using System;

    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class DarkPactUsable : CounterAbility
    {
        public DarkPactUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool BlockPlayersInput(IObstacle obstacle)
        {
            return false;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var requiredTime = base.GetRequiredTime(ally, enemy, obstacle);
            var ability = obstacle.EvadableAbility.Ability;
            if (obstacle.IsModifierObstacle)
            {
                return requiredTime;
            }

            if (ability.IsDisable())
            {
                var remainingTime = obstacle.GetEvadeTime(ally, false);
                //todo add ignore modifier ?
                return Math.Min(this.Ability.ActivationDelay - 0.1f, remainingTime - 0.03f);
            }

            return requiredTime;
        }
    }
}