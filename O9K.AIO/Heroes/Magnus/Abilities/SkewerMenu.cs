namespace O9K.AIO.Heroes.Magnus.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class SkewerMenu : UsableAbilityMenu
    {
        private readonly MenuHeroToggler allyToggler;

        public SkewerMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.allyToggler = this.Menu.Add(new MenuHeroToggler("Skewer to ally", "skewerAllies" + simplifiedName, true));
            this.allyToggler.AddTranslation(Lang.Ru, "Использовать к союзнику");
            this.allyToggler.AddTranslation(Lang.Cn, "向盟友的方向使用");

            this.SkewerToTower = this.Menu.Add(
                new MenuSwitcher("Tower", "skewerTower" + simplifiedName).SetTooltip("Skewer to tower if no allies"));
            this.SkewerToTower.AddTranslation(Lang.Ru, "Использовать к вышке");
            this.SkewerToTower.AddTooltipTranslation(Lang.Ru, "Использовать к вышке, если нет союзников");
            this.SkewerToTower.AddTranslation(Lang.Cn, "在塔的方向使用");
            this.SkewerToTower.AddTooltipTranslation(Lang.Cn, "如果没有盟友，则使用塔的方向");
        }

        public MenuSwitcher SkewerToTower { get; }

        public bool IsAllyEnabled(string heroName)
        {
            return this.allyToggler.IsEnabled(heroName);
        }
    }
}