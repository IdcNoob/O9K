namespace O9K.AutoUsage
{
    using System;
    using System.ComponentModel.Composition;

    using Core.Logger;
    using Core.Managers.Context;

    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using Settings;

    [ExportPlugin("O9K // Auto usage", priority: int.MaxValue)]
    internal class Bootstrap : Plugin
    {
        private readonly IContext9 context;

        private AutoUsage autoUsage;

        private MainSettings settings;

        [ImportingConstructor]
        public Bootstrap(IContext9 context)
        {
            this.context = context;
        }

        protected override void OnActivate()
        {
            try
            {
                this.context.Renderer.TextureManager.LoadFromDota("o9k.glyph", @"panorama\images\hud\reborn\icon_glyph_on_psd.vtex_c");

                this.settings = new MainSettings(this.context.MenuManager);
                this.autoUsage = new AutoUsage(this.settings);
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
                this.autoUsage.Dispose();
                this.settings.Dispose();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}