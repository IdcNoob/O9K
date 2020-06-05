namespace O9K.ItemManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Logger;

    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using Metadata;

    [ExportPlugin("O9K // Item manager", priority: int.MaxValue)]
    internal class Bootstrap : Plugin
    {
        private readonly IEnumerable<IModule> modules;

        [ImportingConstructor]
        public Bootstrap([ImportMany] IEnumerable<IModule> modules)
        {
            this.modules = modules;
        }

        protected override void OnActivate()
        {
            try
            {
                foreach (var module in this.modules.OrderByDescending(x => x is IMainMenu))
                {
                    module.Activate();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        protected override void OnDeactivate()
        {
            try
            {
                foreach (var module in this.modules)
                {
                    module.Dispose();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}