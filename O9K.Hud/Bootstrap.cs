namespace O9K.Hud
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Logger;

    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using Helpers;

    using MainMenu;

    using Modules;

    [ExportPlugin("O9K // Hud", priority: int.MaxValue)]
    internal class Bootstrap : Plugin
    {
        private readonly IEnumerable<IHudModule> modules;

        [ImportingConstructor]
        public Bootstrap([ImportMany] IEnumerable<IHudModule> modules)
        {
            this.modules = modules;
        }

        protected override void OnActivate()
        {
            foreach (var hudModule in this.modules.OrderByDescending(x => x is IHudMenu).ThenByDescending(x => x is IMinimap))
            {
                try
                {
                    hudModule.Activate();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        protected override void OnDeactivate()
        {
            foreach (var hudModule in this.modules)
            {
                try
                {
                    hudModule.Dispose();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }
    }
}