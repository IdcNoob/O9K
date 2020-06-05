namespace O9K.AIO.Heroes.Base
{
    using System;
    using System.Collections.Generic;

    using Core.Entities.Heroes;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu.EventArgs;

    using Ensage.SDK.Helpers;

    using FailSafe;

    using KillStealer;

    using Menu;

    using Modes.Combo;
    using Modes.MoveCombo;

    using ShieldBreaker;

    using TargetManager;

    using UnitManager;

    internal class BaseHero : IDisposable
    {
        private readonly ComboMode combo;

        private readonly MoveComboMode moveCombo;

        public BaseHero(IContext9 context)
        {
            this.Owner = EntityManager9.Owner;

            this.Menu = new MenuManager(this.Owner, context.MenuManager);

            this.TargetManager = new TargetManager(this.Menu);

            this.KillSteal = new KillSteal(this);
            this.FailSafe = new FailSafe(this);
            this.ShieldBreaker = new ShieldBreaker(this);

            this.MoveComboModeMenu = new MoveComboModeMenu(this.Menu.RootMenu, "Move");
            this.ComboMenus.Add(new ComboModeMenu(this.Menu.RootMenu, "Harass"));

            // ReSharper disable once VirtualMemberCallInConstructor
            this.CreateComboMenus();

            this.ShieldBreaker.AddComboMenu(this.ComboMenus);

            // ReSharper disable once VirtualMemberCallInConstructor
            this.CreateUnitManager();

            this.ShieldBreaker.UnitManager = this.UnitManager;

            this.combo = new ComboMode(this, this.ComboMenus);
            this.moveCombo = new MoveComboMode(this, this.MoveComboModeMenu);

            UpdateManager.BeginInvoke(() => this.Menu.Enabled.ValueChange += this.EnabledOnValueChange, 1000);
        }

        public MultiSleeper AbilitySleeper { get; } = new MultiSleeper();

        public List<ComboModeMenu> ComboMenus { get; } = new List<ComboModeMenu>();

        public FailSafe FailSafe { get; }

        public KillSteal KillSteal { get; }

        public MenuManager Menu { get; }

        public MoveComboModeMenu MoveComboModeMenu { get; }

        public MultiSleeper OrbwalkSleeper { get; } = new MultiSleeper();

        public Owner Owner { get; }

        public ShieldBreaker ShieldBreaker { get; }

        public TargetManager TargetManager { get; }

        public UnitManager UnitManager { get; protected set; }

        public virtual void CreateUnitManager()
        {
            this.UnitManager = new UnitManager(this);
        }

        public virtual void Dispose()
        {
            this.Menu.Enabled.ValueChange -= this.EnabledOnValueChange;

            this.ComboMenus.Clear();
            this.combo.Dispose();
            this.moveCombo.Dispose();
            this.KillSteal.Dispose();
            this.FailSafe.Dispose();
            this.ShieldBreaker.Dispose();
            this.UnitManager.Dispose();
            this.Menu.Dispose();
        }

        protected virtual void CreateComboMenus()
        {
            this.ComboMenus.Add(new ComboModeMenu(this.Menu.RootMenu, "Combo"));
            this.ComboMenus.Add(new ComboModeMenu(this.Menu.RootMenu, "Alternative combo"));
        }

        protected virtual void DisableCustomModes()
        {
        }

        protected virtual void EnableCustomModes()
        {
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            try
            {
                if (e.NewValue)
                {
                    this.TargetManager.Enable();
                    this.KillSteal.Enable();
                    this.FailSafe.Enable();
                    this.ShieldBreaker.Enable();
                    this.UnitManager.Enable();
                    this.combo.Enable();
                    this.moveCombo.Enable();
                    this.EnableCustomModes();
                }
                else
                {
                    this.DisableCustomModes();
                    this.TargetManager.Disable();
                    this.KillSteal.Disable();
                    this.FailSafe.Disable();
                    this.ShieldBreaker.Disable();
                    this.UnitManager.Disable();
                    this.combo.Disable();
                    this.moveCombo.Disable();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}