namespace O9K.AIO
{
    using System;
    using System.ComponentModel.Composition;
    using System.Reflection;

    using Core.Entities.Metadata;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;

    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using Heroes.Base;

    [ExportPlugin("O9K // AIO", priority: int.MaxValue)]
    public class Bootstrap : Plugin
    {
        private BaseHero hero;

        [ImportingConstructor]
        public Bootstrap(IContext9 context)
        {
            Context = context;
        }

        public static IContext9 Context { get; private set; }

        protected override void OnActivate()
        {
            try
            {
                var owner = EntityManager9.Owner;
                var type = Array.Find(
                    Assembly.GetExecutingAssembly().GetTypes(),
                    x => !x.IsAbstract && x.IsClass && typeof(BaseHero).IsAssignableFrom(x)
                         && x.GetCustomAttribute<HeroIdAttribute>()?.HeroId == owner.HeroId);

                if (type == null)
                {
                    Logger.Warn("O9K.AIO // Hero is not supported");
                    Logger.Warn("O9K.AIO // Dynamic combo will be loaded");

                    this.hero = new BaseHero(Context);
                    return;
                }

                this.hero = (BaseHero)Activator.CreateInstance(type, Context);
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
                this.hero?.Dispose();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}