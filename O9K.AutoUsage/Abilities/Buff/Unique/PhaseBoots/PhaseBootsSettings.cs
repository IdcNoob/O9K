namespace O9K.AutoUsage.Abilities.Buff.Unique.PhaseBoots
{
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class PhaseBootsSettings
    {
        public PhaseBootsSettings(Menu settings, IActiveAbility ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.Distance = menu.GetOrAdd(new MenuSlider("Distance", 600, 100, 2000));
            this.Distance.SetTooltip("Use phase boots only when move distance is bigger");
            this.Distance.AddTranslation(Lang.Ru, "Дистанция");
            this.Distance.AddTooltipTranslation(Lang.Ru, "Использовать способность, если дистанция передвижения больше");
            this.Distance.AddTranslation(Lang.Cn, "距离");
            this.Distance.AddTooltipTranslation(Lang.Cn, "如果移动距离较长，则使用该能力");
        }

        public MenuSlider Distance { get; }
    }
}