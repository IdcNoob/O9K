namespace O9K.Farm.Menu
{
    using System;

    using O9K.Core.Managers.Context;
    using O9K.Core.Managers.Menu;
    using O9K.Core.Managers.Menu.Items;

    internal class MenuManager : IDisposable
    {
        private readonly IContext9 context;

        private readonly Menu menu;

        public MenuManager(IContext9 context)
        {
            this.context = context;
            this.context.Renderer.TextureManager.LoadFromDota("o9k.icon_gold", @"panorama\images\hud\icon_gold_psd.vtex_c");

            this.menu = new Menu("Farm", "O9K.Farm").SetTexture("o9k.icon_gold");

            this.UnitSettingsMenu = this.menu.Add(new Menu("Units", "units"));
            this.UnitSettingsMenu.AddTranslation(Lang.Ru, "Юниты");
            this.UnitSettingsMenu.AddTranslation(Lang.Cn, "单位");

            this.LastHitMenu = new LastHitMenu(this.menu);
            //  this.PushMenu = new PushMenu(this.menu);
            this.MarkerMenu = new MarkerMenu(this.menu);

            this.context.MenuManager.AddRootMenu(this.menu);
        }

        public LastHitMenu LastHitMenu { get; }

        public MarkerMenu MarkerMenu { get; }

        public Menu UnitSettingsMenu { get; }

        public void Dispose()
        {
            this.context.MenuManager.RemoveRootMenu(this.menu);
        }
    }
}