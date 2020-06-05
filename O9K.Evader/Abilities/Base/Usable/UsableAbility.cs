namespace O9K.Evader.Abilities.Base.Usable
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Managers.Menu.Items;

    using Evadable;

    using Evader.EvadeModes;

    using Metadata;

    using Pathfinder.Obstacles;

    using SharpDX;

    internal abstract class UsableAbility
    {
        protected MenuAbilityToggler Counters;

        protected MenuAbilityToggler ModifierCounters;

        protected UsableAbility(Ability9 ability, IMainMenu menu)
        {
            this.Ability = ability;
            this.ActiveAbility = ability as ActiveAbility;
            this.Menu = menu;
            this.Owner = ability.Owner;
        }

        public Ability9 Ability { get; }

        public ActiveAbility ActiveAbility { get; }

        public Unit9 Owner { get; }

        protected IMainMenu Menu { get; }

        public virtual void AddEvadableAbility(EvadableAbility evadable)
        {
            if (evadable.IsCounteredBy(this))
            {
                this.Counters.AddAbility(evadable.Ability.Name);
            }

            if (evadable.IsModifierCounteredBy(this))
            {
                this.ModifierCounters.AddAbility(evadable.Ability.Name);
            }
        }

        public virtual bool BlockPlayersInput(IObstacle obstacle)
        {
            return true;
        }

        public abstract bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle);

        public abstract float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle);

        public abstract bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle);

        protected virtual bool IsAbilityEnabled(IObstacle obstacle)
        {
            if (obstacle.IsModifierObstacle)
            {
                if (!this.ModifierCounters.IsEnabled(obstacle.EvadableAbility.Ability.Name))
                {
                    return false;
                }
            }
            else
            {
                if (!this.Counters.IsEnabled(obstacle.EvadableAbility.Ability.Name))
                {
                    return false;
                }
            }

            return true;
        }

        protected void MoveCamera(Vector3 position)
        {
            if (!EvadeModeManager.MoveCamera || Hud.IsPositionOnScreen(position))
            {
                return;
            }

            Hud.CameraPosition = position;
        }
    }
}