namespace O9K.Farm
{
    using System;
    using System.ComponentModel.Composition;

    using Core;

    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using Menu;

    using O9K.Core.Logger;
    using O9K.Core.Managers.Context;

    [ExportPlugin("O9K // Farm", priority: int.MaxValue)]
    public class Bootstrap : Plugin
    {
        private readonly IContext9 context;

        private FarmManager farmManager;

        private MenuManager menuManager;

        [ImportingConstructor]
        public Bootstrap(IContext9 context)
        {
            this.context = context;
        }

        protected override void OnActivate()
        {
            try
            {
                this.menuManager = new MenuManager(this.context);
                this.farmManager = new FarmManager(this.context, this.menuManager);
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
                this.farmManager.Dispose();
                this.menuManager.Dispose();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}