namespace O9K.Core.Plugins.OrderBlocker
{
    using System;

    using Ensage;

    using Managers.Assembly;
    using Managers.Context;
    using Managers.Menu;
    using Managers.Menu.EventArgs;
    using Managers.Menu.Items;

    internal class OrderBlockerMenu : IDisposable
    {
        private readonly IAssemblyEventManager9 eventManager;

        private readonly Menu menu;

        private readonly IMenuManager9 menuManager;

        public OrderBlockerMenu(IContext9 context, IMenuManager9 menuManager, IAssemblyEventManager9 eventManager)
        {
            this.menuManager = menuManager;
            this.eventManager = eventManager;
            context.Renderer.TextureManager.LoadFromDota("o9k.block", @"panorama\images\hud\reborn\ping_icon_retreat_psd.vtex_c");

            this.menu = new Menu("Orders blocker").SetTexture("o9k.block");

            this.OfScreenBlock = this.menu.Add(new MenuSwitcher("Block off screen"));
            this.MoveCamera = this.menu.Add(new MenuSwitcher("Move camera when off screen").SetTooltip("Block off screen must be enabled"));
            this.SpamBlock = this.menu.Add(new MenuSwitcher("Block spam"));
            this.BlockTooFastReaction = this.menu.Add(new MenuSwitcher("Block too fast reaction"));
            this.BlockNotSelectUnits = this.menu.Add(new MenuSwitcher("Block not selected units"));
            //  this.SelectUnits = this.menu.Add(new MenuSwitcher("Select units").SetTooltip("Block not selected units must be enabled"));
            this.SelectUnits = new MenuSwitcher("dummy", false);
            this.ZoomBlock = this.menu.Add(new MenuSwitcher("Block zoom hack"));
            this.SlowDown = this.menu.Add(new MenuSwitcher("Slow down combos", false));
            this.ShowInfo = this.menu.Add(new MenuSwitcher("Show info", false));

            this.OfScreenBlock.ValueChange += this.OfScreenBlock_OnValueChange;
            this.MoveCamera.ValueChange += this.MoveCamera_OnValueChange;

            this.BlockNotSelectUnits.ValueChange += this.BlockNotSelectUnits_OnValueChange;
            this.SelectUnits.ValueChange += this.SelectUnits_OnValueChange;

            this.menuManager.AddRootMenu(this.menu);

            this.eventManager.InvokeOrderBlockerMoveCamera(this.MoveCamera.IsEnabled);
        }

        public MenuSwitcher BlockNotSelectUnits { get; }

        public MenuSwitcher BlockTooFastReaction { get; }

        public MenuSwitcher MoveCamera { get; }

        public MenuSwitcher OfScreenBlock { get; }

        public MenuSwitcher SelectUnits { get; }

        public MenuSwitcher ShowInfo { get; }

        public MenuSwitcher SlowDown { get; }

        public MenuSwitcher SpamBlock { get; }

        public MenuSwitcher ZoomBlock { get; }

        public void Dispose()
        {
            this.MoveCamera.ValueChange -= this.MoveCamera_OnValueChange;
            this.OfScreenBlock.ValueChange -= this.OfScreenBlock_OnValueChange;
            this.BlockNotSelectUnits.ValueChange -= this.BlockNotSelectUnits_OnValueChange;
            this.SelectUnits.ValueChange -= this.SelectUnits_OnValueChange;

            this.menuManager.RemoveRootMenu(this.menu);
        }

        private void BlockNotSelectUnits_OnValueChange(object sender, SwitcherEventArgs e)
        {
            if (!e.NewValue)
            {
                this.SelectUnits.IsEnabled = false;
            }
        }

        private void HumanaizerOnValueChange(object sender, SwitcherEventArgs e)
        {
            Config.HumanizerEnabled = e.NewValue;
        }

        private void MoveCamera_OnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue && !this.OfScreenBlock)
            {
                e.Process = false;
                return;
            }

            this.eventManager.InvokeOrderBlockerMoveCamera(e.NewValue);
        }

        private void OfScreenBlock_OnValueChange(object sender, SwitcherEventArgs e)
        {
            if (!e.NewValue)
            {
                this.MoveCamera.IsEnabled = false;
            }
        }

        private void SelectUnits_OnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue && !this.BlockNotSelectUnits)
            {
                e.Process = false;
                return;
            }

            // send select unit event  ?
            // this.eventManager.InvokeOrderBlockerMoveCamera(e.NewValue);
        }
    }
}