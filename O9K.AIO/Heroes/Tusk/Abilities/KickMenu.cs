namespace O9K.AIO.Heroes.Tusk.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class KickMenu : UsableAbilityMenu
    {
        public KickMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.KickToAlly = this.Menu.Add(new MenuSwitcher("Kick to ally", ability.DefaultName + "kick" + simplifiedName, true));
            this.KickToAlly.AddTranslation(Lang.Ru, "Пинать к союзнику");
            this.KickToAlly.AddTranslation(Lang.Cn, "向盟友的方向使用");
        }

        public MenuSwitcher KickToAlly { get; }
    }
}