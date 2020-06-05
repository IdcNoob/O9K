namespace O9K.AIO.Abilities.Menus
{
    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu.Items;

    internal class UsableAbilityMenu
    {
        public UsableAbilityMenu(Ability9 ability, string simplifiedName)
        {
            this.Menu = new Menu(ability.DisplayName, "settings" + ability.Name + simplifiedName);
            this.Menu.TextureKey = ability.Name;
        }

        public Menu Menu { get; }
    }
}