namespace O9K.AIO.Modes.Base
{
    using System;

    using Core.Entities.Heroes;

    using Heroes.Base;

    using Menu;

    using TargetManager;

    internal abstract class BaseMode : IDisposable
    {
        protected BaseMode(BaseHero baseHero)
        {
            this.BaseHero = baseHero;
            this.Owner = baseHero.Owner;
            this.Menu = baseHero.Menu;
            this.TargetManager = baseHero.TargetManager;
        }

        protected BaseHero BaseHero { get; }

        protected MenuManager Menu { get; }

        protected Owner Owner { get; }

        protected TargetManager TargetManager { get; }

        public virtual void Dispose()
        {
        }
    }
}