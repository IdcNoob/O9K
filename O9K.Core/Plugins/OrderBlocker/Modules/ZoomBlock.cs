namespace O9K.Core.Plugins.OrderBlocker.Modules
{
    using System;

    using Data;

    using Ensage;

    using Logger;

    using Managers.Menu.EventArgs;

    internal class ZoomBlock : IDisposable
    {
        private readonly OrderBlockerMenu menu;

        public ZoomBlock(OrderBlockerMenu menu)
        {
            this.menu = menu;

            menu.ZoomBlock.ValueChange += this.ZoomBlockOnValueChange;
        }

        public void Dispose()
        {
            this.menu.ZoomBlock.ValueChange -= this.ZoomBlockOnValueChange;
            Game.OnConVarChanged -= this.OnConVarChanged;
        }

        private void OnConVarChanged(ConVarChangedEventArgs args)
        {
            try
            {
                if (args.ConVar.Name == "dota_camera_distance" && args.ConVar.GetInt() != GameData.DefaultZoom)
                {
                    Game.GetConsoleVar("dota_camera_distance").SetValue(GameData.DefaultZoom);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void ZoomBlockOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                Game.GetConsoleVar("dota_camera_distance").SetValue(GameData.DefaultZoom);
                Game.OnConVarChanged += this.OnConVarChanged;
            }
            else
            {
                Game.OnConVarChanged -= this.OnConVarChanged;
            }
        }
    }
}