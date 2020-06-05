namespace O9K.Evader.Abilities.Base.Usable.CounterAbility
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class CounterAbility : UsableAbility
    {
        public CounterAbility(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            var settings = menu.AbilitySettings.AddAbilitySettingsMenu(ability);

            this.Counters = settings.GetOrAdd(new MenuAbilityToggler("Counter:"));
            this.Counters.AddTranslation(Lang.Ru, "Контр:");
            this.Counters.AddTranslation(Lang.Cn, "应对:");

            this.ModifierCounters = settings.GetOrAdd(new MenuAbilityToggler("Modifier counter:"));
            this.ModifierCounters.AddTranslation(Lang.Ru, "Контр модификатора:");
            this.ModifierCounters.AddTranslation(Lang.Cn, "应对特效:");

            this.IsToggleable = ability is IToggleable;

            switch (ability)
            {
                case IShield shield:
                {
                    this.CanBeCastedOnAlly = shield.ShieldsAlly;
                    this.CanBeCastedOnSelf = shield.ShieldsOwner;
                    this.ModifierName = shield.ShieldModifierName;
                    return;
                }
                case IBuff buff:
                {
                    this.CanBeCastedOnAlly = buff.BuffsAlly;
                    this.CanBeCastedOnSelf = buff.BuffsOwner;
                    this.ModifierName = buff.BuffModifierName;
                    return;
                }
                case IHealthRestore healthRestore:
                {
                    this.CanBeCastedOnAlly = healthRestore.RestoresAlly;
                    this.CanBeCastedOnSelf = healthRestore.RestoresOwner;
                    this.ModifierName = healthRestore.RestoreModifierName;
                    return;
                }
            }

            this.CanBeCastedOnSelf = true;
        }

        public bool IsToggleable { get; }

        protected bool CanBeCastedOnAlly { get; set; }

        protected bool CanBeCastedOnSelf { get; set; }

        protected string ModifierName { get; set; }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!this.IsAbilityEnabled(obstacle))
            {
                return false;
            }

            if (!this.CanBeCastedOnSelf && ally.Equals(this.Owner))
            {
                return false;
            }

            if (!this.CanBeCastedOnAlly && !ally.Equals(this.Owner))
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

            if (this.Owner.IsInvisible && this.Owner.IsImportant && !ally.Equals(this.Owner) && !this.Owner.CanUseAbilitiesInInvisibility
                && !this.Owner.HasModifier("modifier_item_glimmer_cape_fade"))
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
            this.MoveCamera(ally.Position);
            return this.ActiveAbility.UseAbility(ally, false, true);
        }

        protected override bool IsAbilityEnabled(IObstacle obstacle)
        {
            if (!this.Menu.AbilitySettings.IsCounterEnabled(this.Ability.Name))
            {
                return false;
            }

            return base.IsAbilityEnabled(obstacle);
        }
    }
}