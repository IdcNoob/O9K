namespace O9K.AIO.Modes.Combo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Base;

    using Core.Entities.Abilities.Base.Components;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Heroes.Base;

    using UnitManager;

    internal class ComboMode : BaseMode
    {
        private readonly Dictionary<MenuHoldKey, ComboModeMenu> comboModeMenus = new Dictionary<MenuHoldKey, ComboModeMenu>();

        private readonly List<uint> disableToggleAbilities = new List<uint>();

        private readonly HashSet<AbilityId> ignoreToggleDisable = new HashSet<AbilityId>
        {
            AbilityId.troll_warlord_berserkers_rage
        };

        private readonly IUpdateHandler updateHandler;

        private bool ignoreComboEnd;

        public ComboMode(BaseHero baseHero, IEnumerable<ComboModeMenu> comboMenus)
            : base(baseHero)
        {
            this.UnitManager = baseHero.UnitManager;
            this.updateHandler = UpdateManager.Subscribe(this.OnUpdate, 0, false);

            foreach (var comboMenu in comboMenus)
            {
                this.comboModeMenus.Add(comboMenu.Key, comboMenu);
            }
        }

        protected ComboModeMenu ComboModeMenu { get; private set; }

        protected UnitManager UnitManager { get; }

        public void Disable()
        {
            this.updateHandler.IsEnabled = false;
            Player.OnExecuteOrder -= this.OnExecuteOrder;

            foreach (var comboMenu in this.comboModeMenus)
            {
                comboMenu.Key.ValueChange -= this.KeyOnValueChanged;
            }
        }

        public override void Dispose()
        {
            UpdateManager.Unsubscribe(this.updateHandler);
            Player.OnExecuteOrder -= this.OnExecuteOrder;

            foreach (var comboMenu in this.comboModeMenus)
            {
                comboMenu.Key.ValueChange -= this.KeyOnValueChanged;
            }
        }

        public void Enable()
        {
            Player.OnExecuteOrder += this.OnExecuteOrder;

            foreach (var comboMenu in this.comboModeMenus)
            {
                comboMenu.Key.ValueChange += this.KeyOnValueChanged;
            }
        }

        protected void ComboEnd()
        {
            try
            {
                foreach (var abilityHandle in this.disableToggleAbilities.Distinct().ToList())
                {
                    if (!(EntityManager9.GetAbility(abilityHandle) is IToggleable ability))
                    {
                        continue;
                    }

                    UpdateManager.BeginInvoke(() => this.ToggleAbility(ability));
                }

                this.UnitManager.EndCombo(this.ComboModeMenu);
                this.disableToggleAbilities.Clear();
            }
            catch (Exception e)
            {
                Logger.Error(e);
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
                if (this.TargetManager.HasValidTarget)
                {
                    this.UnitManager.ExecuteCombo(this.ComboModeMenu);
                }

                this.UnitManager.Orbwalk(this.ComboModeMenu);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void KeyOnValueChanged(object sender, KeyEventArgs e)
        {
            if (e.NewValue)
            {
                if (this.updateHandler.IsEnabled)
                {
                    this.ignoreComboEnd = true;
                }

                this.ComboModeMenu = this.comboModeMenus[(MenuHoldKey)sender];
                this.TargetManager.TargetLocked = true;
                this.updateHandler.IsEnabled = true;
            }
            else
            {
                if (this.ignoreComboEnd)
                {
                    this.ignoreComboEnd = false;
                    return;
                }

                this.updateHandler.IsEnabled = false;
                this.TargetManager.TargetLocked = false;
                this.ComboEnd();
            }
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!this.updateHandler.IsEnabled || !args.Process || args.IsPlayerInput)
                {
                    return;
                }

                switch (args.OrderId)
                {
                    case OrderId.ToggleAutoCast:
                    case OrderId.ToggleAbility:
                    {
                        if (this.ignoreToggleDisable.Contains(args.Ability.Id))
                        {
                            return;
                        }

                        this.disableToggleAbilities.Add(args.Ability.Handle);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private async void ToggleAbility(IToggleable toggle)
        {
            try
            {
                if (!toggle.Enabled)
                {
                    await Task.Delay(200);
                }

                while (toggle.IsValid && toggle.Enabled && !this.updateHandler.IsEnabled)
                {
                    if (toggle.CanBeCasted() && !toggle.Owner.IsCasting)
                    {
                        toggle.Enabled = false;
                        break;
                    }

                    await Task.Delay(200);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}