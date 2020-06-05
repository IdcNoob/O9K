namespace O9K.AIO.Heroes.Oracle.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class FortunesEndMenu : UsableAbilityMenu
    {
        public FortunesEndMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.FullChannelTime = this.Menu.Add(new MenuSwitcher("Full channel time", "fortunesEndFullTime" + simplifiedName, false));
            this.FullChannelTime.AddTranslation(Lang.Ru, "Ждать до конца каста");
            this.FullChannelTime.AddTranslation(Lang.Cn, "等待直到使用结束");
        }

        public MenuSwitcher FullChannelTime { get; }
    }
}