namespace O9K.Evader.Abilities.Heroes.Ursa.Enrage
{
    using System.Linq;

    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Extensions;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class EnrageUsable : CounterAbility
    {
        public EnrageUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            if (!ally.HasAghanimsScepter)
            {
                return true;
            }

            if (obstacle.IsModifierObstacle)
            {
                return true;
            }

            if (!(obstacle.EvadableAbility.Ability is IDisable disable))
            {
                return true;
            }

            if (!disable.IsStun() || Abilities.FullAoeDisable.Contains(disable.Id))
            {
                return true;
            }

            return false;
        }
    }
}