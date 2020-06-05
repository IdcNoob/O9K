namespace O9K.AutoUsage.Abilities.Special.Unique.IronTalon
{
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class IronTalonSettings
    {
        public IronTalonSettings(Menu settings, IActiveAbility ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.Damage = menu.GetOrAdd(new MenuSlider("Minimal damage", 300, 100, 1000));
            this.Damage.AddTranslation(Lang.Ru, "Минимальный урон");
            this.Damage.AddTranslation(Lang.Cn, "最小伤害");
        }

        public MenuSlider Damage { get; }
    }
}