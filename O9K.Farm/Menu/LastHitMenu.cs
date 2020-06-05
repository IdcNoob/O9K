namespace O9K.Farm.Menu
{
    using O9K.Core.Managers.Menu;
    using O9K.Core.Managers.Menu.Items;

    internal class LastHitMenu
    {
        public LastHitMenu(Menu rootMenu)
        {
            var menu = new Menu("Last hit", "lastHit");
            menu.AddTranslation(Lang.Ru, "Ласт хит");
            menu.AddTranslation(Lang.Cn, "最后一击");

            this.ToggleKey = menu.Add(new MenuHoldKey("Toggle key", "toggle"));
            this.ToggleKey.AddTranslation(Lang.Ru, "Клавиша переключения");
            this.ToggleKey.AddTranslation(Lang.Cn, "切换键");

            this.HoldKey = menu.Add(new MenuHoldKey("Hold key", "hold"));
            this.HoldKey.AddTranslation(Lang.Ru, "Клавиша удержания");
            this.HoldKey.AddTranslation(Lang.Cn, "按住键");

            rootMenu.Add(menu);
        }

        public MenuHoldKey HoldKey { get; }

        public MenuHoldKey ToggleKey { get; }
    }
}