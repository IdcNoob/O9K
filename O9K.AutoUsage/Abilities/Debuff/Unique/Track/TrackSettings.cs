namespace O9K.AutoUsage.Abilities.Debuff.Unique.Track
{
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class TrackSettings
    {
        public TrackSettings(Menu settings, IActiveAbility ability)
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

            this.HpThreshold = menu.GetOrAdd(new MenuSlider("HP% threshold (target)", 40, 1, 100));
            this.HpThreshold.SetTooltip("Use when target hp% is lower");
            this.HpThreshold.AddTranslation(Lang.Ru, "Порог ХП%");
            this.HpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда у врага меньше здоровья%");
            this.HpThreshold.AddTranslation(Lang.Cn, "生命值％（目标）");
            this.HpThreshold.AddTooltipTranslation(Lang.Cn, "当目标健康状况较差 % 时，使用能力");

            this.ForceInvisibility = menu.GetOrAdd(new MenuSwitcher("Force use (invisibility)"));
            this.ForceInvisibility.SetTooltip("Always use ability when enemy can become invisible");
            this.ForceInvisibility.AddTranslation(Lang.Ru, "Невидимость");
            this.ForceInvisibility.AddTooltipTranslation(Lang.Ru, "Использовать способность всегда, когда враг может стать невидимым");
            this.ForceInvisibility.AddTranslation(Lang.Cn, "隐身");
            this.ForceInvisibility.AddTooltipTranslation(Lang.Cn, "当敌人变得不可见时，总是使用能力");
        }

        public MenuSwitcher ForceInvisibility { get; }

        public MenuSlider HpThreshold { get; }

        public MenuSwitcher OnAttack { get; }

        public MenuSwitcher OnSight { get; }
    }
}