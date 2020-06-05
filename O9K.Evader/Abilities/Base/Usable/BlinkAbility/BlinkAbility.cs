namespace O9K.Evader.Abilities.Base.Usable.BlinkAbility
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Metadata;

    using Pathfinder.Obstacles;

    using SharpDX;

    internal class BlinkAbility : UsableAbility
    {
        public BlinkAbility(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, menu)
        {
            var settings = menu.AbilitySettings.AddAbilitySettingsMenu(ability);

            this.Counters = settings.GetOrAdd(new MenuAbilityToggler("Blink:"));
            this.Counters.AddTranslation(Lang.Ru, "Блинк:");
            this.Counters.AddTranslation(Lang.Cn, "闪烁:");

            this.ModifierCounters = settings.GetOrAdd(new MenuAbilityToggler("Modifier blink:"));
            this.ModifierCounters.AddTranslation(Lang.Ru, "Блинк модификатора:");
            this.ModifierCounters.AddTranslation(Lang.Cn, "闪烁特效:");

            this.Pathfinder = pathfinder;
            this.FountainPosition = EntityManager9.AllyFountain;
        }

        protected Vector3 FountainPosition { get; }

        protected IPathfinder Pathfinder { get; }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!this.IsAbilityEnabled(obstacle))
            {
                return false;
            }

            if (!ally.Equals(this.Owner))
            {
                return false;
            }

            return this.Ability.CanBeCasted(false);
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.GetCastDelay(this.FountainPosition) + 0.05f;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var position = this.Owner.Position.Extend2D(this.FountainPosition, this.ActiveAbility.CastRange);
            this.MoveCamera(position);
            return this.ActiveAbility.UseAbility(position, false, true);
        }

        protected override bool IsAbilityEnabled(IObstacle obstacle)
        {
            if (!this.Menu.AbilitySettings.IsBlinkEnabled(this.Ability.Name))
            {
                return false;
            }

            return base.IsAbilityEnabled(obstacle);
        }
    }
}