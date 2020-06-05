namespace O9K.AIO.Modes.KeyPress
{
    using System;

    using Base;

    using Core.Logger;
    using Core.Managers.Menu.EventArgs;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Heroes.Base;

    using UnitManager;

    internal abstract class KeyPressMode : BaseMode
    {
        private readonly KeyPressModeMenu menu;

        protected KeyPressMode(BaseHero baseHero, KeyPressModeMenu menu)
            : base(baseHero)
        {
            this.UnitManager = baseHero.UnitManager;
            this.menu = menu;

            this.UpdateHandler = UpdateManager.Subscribe(this.OnUpdate, 0, false);
        }

        protected bool LockTarget { get; set; } = true;

        protected UnitManager UnitManager { get; }

        protected IUpdateHandler UpdateHandler { get; }

        public virtual void Disable()
        {
            this.UpdateHandler.IsEnabled = false;
            this.menu.Key.ValueChange -= this.KeyOnValueChanged;
        }

        public override void Dispose()
        {
            base.Dispose();
            this.menu.Key.ValueChange -= this.KeyOnValueChanged;
        }

        public virtual void Enable()
        {
            this.menu.Key.ValueChange += this.KeyOnValueChanged;
        }

        protected abstract void ExecuteCombo();

        protected virtual void KeyOnValueChanged(object sender, KeyEventArgs e)
        {
            if (e.NewValue)
            {
                if (this.LockTarget)
                {
                    this.TargetManager.TargetLocked = true;
                }

                this.UpdateHandler.IsEnabled = true;
            }
            else
            {
                this.UpdateHandler.IsEnabled = false;

                if (this.LockTarget)
                {
                    this.TargetManager.TargetLocked = false;
                }
            }
        }

        protected void OnUpdate()
        {
            if (Game.IsPaused)
            {
                return;
            }

            try
            {
                this.ExecuteCombo();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}