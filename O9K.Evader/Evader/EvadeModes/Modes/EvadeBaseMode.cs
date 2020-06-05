namespace O9K.Evader.Evader.EvadeModes.Modes
{
    using Abilities.Base.Usable;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal abstract class EvadeBaseMode
    {
        protected const float EvadeTiming = 0.1f;

        protected readonly IActionManager ActionManager;

        protected EvadeBaseMode(IActionManager actionManager)
        {
            this.ActionManager = actionManager;
        }

        public abstract EvadeResult Evade(Unit9 ally, IObstacle obstacle);

        protected bool ChannelCanceled(Unit9 ally, IObstacle obstacle, float remainingTime, UsableAbility usableAbility)
        {
            if (!ally.IsChanneling || !ally.IsControllable)
            {
                return true;
            }

            if (usableAbility?.Owner?.Equals(ally) == false)
            {
                return true;
            }

            if (usableAbility?.ActiveAbility?.CanBeCastedWhileChanneling == true)
            {
                return true;
            }

            var isStun = obstacle.EvadableAbility.Ability is IDisable disable && disable.IsStun();

            if (!isStun)
            {
                return false;
            }

            if (ally.IsTeleporting && ally.ChannelEndTime < Game.RawGameTime + remainingTime)
            {
                return false;
            }

            ally.BaseUnit.Stop(false, true);
            return true;
        }

        protected bool PhaseCanceled(Unit9 ally, IObstacle obstacle, UsableAbility usableAbility)
        {
            if (!ally.IsCasting || !ally.IsControllable)
            {
                return true;
            }

            if (usableAbility?.Owner?.Equals(ally) == false)
            {
                return true;
            }

            var isDisable = obstacle.EvadableAbility.Ability is IDisable disable && (disable.IsStun() || disable.IsSilence());

            if (!isDisable)
            {
                return false;
            }

            ally.BaseUnit.Stop(false, true);
            return true;
        }
    }
}