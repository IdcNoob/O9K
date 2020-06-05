namespace O9K.AutoUsage.Abilities.Special.Unique.Midas
{
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class MidasSettings
    {
        public MidasSettings(Menu settings, IActiveAbility ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.Experience = menu.GetOrAdd(new MenuSlider("Experience gain", 80, 0, 150));
            this.Experience.AddTranslation(Lang.Ru, "Получение опыта");
            this.Experience.AddTranslation(Lang.Cn, "获得经验");
        }

        public MenuSlider Experience { get; }
    }
}