namespace O9K.Evader.Abilities.Base.Usable.DisableAbility
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;
    using Core.Prediction.Data;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class DisableAbility : UsableAbility
    {
        public DisableAbility(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            var settings = menu.AbilitySettings.AddAbilitySettingsMenu(ability);

            this.Counters = settings.GetOrAdd(new MenuAbilityToggler("Disable:"));
            this.Counters.AddTranslation(Lang.Ru, "Дизейбл:");
            this.Counters.AddTranslation(Lang.Cn, "控制:");

            this.ModifierCounters = settings.GetOrAdd(new MenuAbilityToggler("Modifier disable:"));
            this.ModifierCounters.AddTranslation(Lang.Ru, "Дизейбл модификатора:");
            this.ModifierCounters.AddTranslation(Lang.Cn, "控制特效:");

            if (ability is IDisable disable)
            {
                this.AppliesUnitState = disable.AppliesUnitState;
            }
        }

        public UnitState AppliesUnitState { get; }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!this.IsAbilityEnabled(obstacle))
            {
                return false;
            }

            if (this.ActiveAbility.UnitTargetCast && !enemy.IsVisible)
            {
                return false;
            }

            if (this.ActiveAbility.BreaksLinkens && enemy.IsBlockingAbilities)
            {
                return false;
            }

            if (!this.Ability.CanBeCasted(false))
            {
                return false;
            }

            if (!enemy.IsVisible)
            {
                return false;
            }

            if (!this.ActiveAbility.CanHit(enemy))
            {
                return false;
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.GetCastDelay(enemy.Position);
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(enemy.Position);
            var use = this.ActiveAbility.UseAbility(enemy, HitChance.Medium, false, true);
            if (use)
            {
                enemy.SetExpectedUnitState(this.AppliesUnitState, this.ActiveAbility.GetHitTime(enemy) + 0.3f);
            }

            return use;
        }

        protected override bool IsAbilityEnabled(IObstacle obstacle)
        {
            if (!this.Menu.AbilitySettings.IsDisableEnabled(this.Ability.Name))
            {
                return false;
            }

            return base.IsAbilityEnabled(obstacle);
        }
    }
}