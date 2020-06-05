namespace O9K.AutoUsage.Abilities.Debuff
{
    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;
    using Core.Prediction.Collision;

    internal class DebuffSettings
    {
        private readonly MenuHeroToggler heroes;

        public DebuffSettings(Menu settings, IActiveAbility ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.OnSight = menu.GetOrAdd(new MenuSwitcher("Use on sight", false));
            this.OnSight.SetTooltip("Use ability when target is visible");
            this.OnSight.AddTranslation(Lang.Ru, "Использовать сразу");
            this.OnSight.AddTooltipTranslation(Lang.Ru, "Использовать способность сразу, когда враг появляется в зоне видимости");
            this.OnSight.AddTranslation(Lang.Cn, "立即使用");
            this.OnSight.AddTooltipTranslation(Lang.Cn, "当目标出现在视线中时，立即使用该能力");

            this.OnAttack = menu.GetOrAdd(new MenuSwitcher("Use on attack"));
            this.OnAttack.SetTooltip("Use ability when hero is attacking");
            this.OnAttack.AddTranslation(Lang.Ru, "Использовать при атаке");
            this.OnAttack.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда герой атакует");
            this.OnAttack.AddTranslation(Lang.Cn, "在攻击时使用");
            this.OnAttack.AddTooltipTranslation(Lang.Cn, "当英雄攻击时使用能力");

            this.SelfOnly = menu.GetOrAdd(new MenuSwitcher("Self only"));
            this.SelfOnly.SetTooltip("Use only when target is near your hero");
            this.SelfOnly.AddTranslation(Lang.Ru, "Только возле себя");
            this.SelfOnly.AddTooltipTranslation(Lang.Ru, "Использовать способность, только если враг возле вашего героя");
            this.SelfOnly.AddTranslation(Lang.Cn, "仅自己");
            this.SelfOnly.AddTooltipTranslation(Lang.Cn, "仅当目标靠近您的英雄时才使用");

            this.MaxCastRange = menu.GetOrAdd(new MenuSlider("Max cast range", 0, 0, 3000));
            this.MaxCastRange.SetTooltip("Use only when enemy is in range");
            this.MaxCastRange.AddTranslation(Lang.Ru, "Максимальная дистанция");
            this.MaxCastRange.AddTooltipTranslation(Lang.Ru, "Максимальная дистанция использования способности");
            this.MaxCastRange.AddTranslation(Lang.Cn, "最大距离");
            this.MaxCastRange.AddTooltipTranslation(Lang.Cn, "使用能力的最大距离");

            if (ability is IHasRadius && ability.CollisionTypes == CollisionTypes.None)
            {
                this.EnemiesCount = menu.GetOrAdd(new MenuSlider("Number of enemies", 1, 1, 5));
                this.EnemiesCount.SetTooltip("Use only when you can debuff equals/more enemies");
                this.EnemiesCount.AddTranslation(Lang.Ru, "Число врагов");
                this.EnemiesCount.AddTooltipTranslation(Lang.Ru, "Использовать способность, если попадет по больше или равно врагов");
                this.EnemiesCount.AddTranslation(Lang.Cn, "敌人数量");
                this.EnemiesCount.AddTooltipTranslation(Lang.Cn, "仅在附近有相等/更多敌人的情况下使用");
            }
            else
            {
                this.EnemiesCount = new MenuSlider("Number of enemies", 0, 0, 0);
            }

            this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use on:", false, true));
            this.heroes.AddTranslation(Lang.Ru, "Использовать на:");
            this.heroes.AddTranslation(Lang.Cn, "用于：");
        }

        public MenuSlider EnemiesCount { get; }

        public MenuSlider MaxCastRange { get; }

        public MenuSwitcher OnAttack { get; }

        public MenuSwitcher OnSight { get; }

        public MenuSwitcher SelfOnly { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes.IsEnabled(name);
        }
    }
}