namespace O9K.ItemManager.Modules.AutoActions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading.Tasks;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Assembly;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using OrderHelper;

    using SharpDX;

    using Attribute = Ensage.Attribute;

    internal class PowerTreads : IModule
    {
        private const int Delay = 50;

        private readonly MenuAbilityToggler agiToggler;

        private readonly Sleeper changeBackSleeper = new Sleeper();

        private readonly MenuSwitcher enabled;

        private readonly IAssemblyEventManager9 eventManager;

        private readonly HashSet<AbilityId> forceChangeBackAbilities = new HashSet<AbilityId>
        {
            AbilityId.naga_siren_mirror_image,
            AbilityId.chaos_knight_phantasm,
            AbilityId.item_manta,
        };

        private readonly Sleeper forceChangeBackSleeper = new Sleeper();

        private readonly HashSet<AbilityId> ignoredAbilities = new HashSet<AbilityId>
        {
            AbilityId.item_tpscroll,
            AbilityId.item_travel_boots,
            AbilityId.item_travel_boots_2
        };

        private readonly MenuAbilityToggler intToggler;

        private readonly MenuSwitcher manualOnly;

        private readonly IOrderSync orderSync;

        private readonly MenuHoldKey recoveryKey;

        private Attribute defaultAttribute;

        private bool ignoreNextOrder;

        private Owner owner;

        private Core.Entities.Abilities.Items.PowerTreads powerTreads;

        private bool subscribed;

        private bool switchingThreads;

        [ImportingConstructor]
        public PowerTreads(IMainMenu mainMenu, IAssemblyEventManager9 eventManager, IOrderSync orderSync)
        {
            this.eventManager = eventManager;
            this.orderSync = orderSync;

            var menu = mainMenu.AutoActionsMenu.Add(new Menu(LocalizationHelper.LocalizeName(AbilityId.item_power_treads), "PowerTreads"));

            this.enabled = menu.Add(new MenuSwitcher("Enabled"));
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTranslation(Lang.Cn, "启用");

            this.manualOnly = menu.Add(new MenuSwitcher("Manual only", false).SetTooltip("Use only when abilities are casted manually"));
            this.manualOnly.AddTranslation(Lang.Ru, "Только вручную");
            this.manualOnly.AddTranslation(Lang.Cn, "仅手册");
            this.manualOnly.AddTooltipTranslation(Lang.Ru, "Использовать только когда способности кастуются вручную");
            this.manualOnly.AddTooltipTranslation(Lang.Cn, "仅在手动使用技能时使用");

            this.intToggler = menu.Add(new MenuAbilityToggler("Intelligence"));
            this.intToggler.AddTranslation(Lang.Ru, "Инт");
            this.intToggler.AddTranslation(Lang.Cn, "智力");

            this.agiToggler = menu.Add(new MenuAbilityToggler("Agility"));
            this.agiToggler.AddTranslation(Lang.Ru, "Агила");
            this.agiToggler.AddTranslation(Lang.Cn, "敏捷");

            // get recovery key
            this.recoveryKey = mainMenu.RecoveryAbuseMenu.GetOrAdd(new MenuHoldKey("Key"));
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;

            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            this.eventManager.AutoSoulRingEnabled -= this.OnAutoSoulRingEnabled;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            Player.OnExecuteOrder -= this.OnExecutePowerTreadsOrder;
            UpdateManager.Unsubscribe(this.OnUpdate);
            this.subscribed = false;
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.AbilityAdded += this.OnAbilityAdded;
                EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
                this.eventManager.AutoSoulRingEnabled += this.OnAutoSoulRingEnabled;
            }
            else
            {
                EntityManager9.AbilityAdded -= this.OnAbilityAdded;
                EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
                Player.OnExecuteOrder -= this.OnExecuteOrder;
                Player.OnExecuteOrder -= this.OnExecutePowerTreadsOrder;
                this.eventManager.AutoSoulRingEnabled -= this.OnAutoSoulRingEnabled;
                UpdateManager.Unsubscribe(this.OnUpdate);
                this.subscribed = false;
            }
        }

        private int GetPowerTreadsSwitchCount(Ability9 ability, Vector3 position)
        {
            var currentAttribute = this.powerTreads.ActiveAttribute;

            if (this.intToggler.IsEnabled(ability.Name))
            {
                if (currentAttribute != Attribute.Intelligence)
                {
                    return currentAttribute == Attribute.Agility ? 2 : 1;
                }
            }
            else if (this.agiToggler.IsEnabled(ability.Name))
            {
                if (currentAttribute != Attribute.Agility)
                {
                    return currentAttribute == Attribute.Strength ? 2 : 1;
                }
            }

            if (currentAttribute != this.defaultAttribute)
            {
                this.SetSleep(ability, position);
            }

            return 0;
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!ability.Owner.IsMyHero || !(ability is ActiveAbility))
                {
                    return;
                }

                if (ability.Id == AbilityId.item_power_treads)
                {
                    if (this.subscribed)
                    {
                        return;
                    }

                    this.powerTreads = (Core.Entities.Abilities.Items.PowerTreads)ability;
                    this.defaultAttribute = this.powerTreads.ActiveAttribute;

                    Player.OnExecuteOrder += this.OnExecutePowerTreadsOrder;
                    Player.OnExecuteOrder += this.OnExecuteOrder;
                    UpdateManager.Subscribe(this.OnUpdate, 112);
                    this.eventManager.InvokeForceBlockerResubscribe();
                    this.subscribed = true;
                }
                else if (ability.BaseAbility.GetManaCost(1) > 0 && !this.ignoredAbilities.Contains(ability.Id))
                {
                    this.intToggler.AddAbility(ability.Id);
                    this.agiToggler.AddAbility(ability.Id, false);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            try
            {
                if (!ability.Owner.IsMyHero)
                {
                    return;
                }

                if (ability.Handle != this.powerTreads?.Handle)
                {
                    return;
                }

                Player.OnExecuteOrder -= this.OnExecuteOrder;
                Player.OnExecuteOrder -= this.OnExecutePowerTreadsOrder;
                UpdateManager.Unsubscribe(this.OnUpdate);
                this.subscribed = false;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAutoSoulRingEnabled(object sender, EventArgs e)
        {
            if (!this.subscribed)
            {
                return;
            }

            // change execute order check
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            Player.OnExecuteOrder += this.OnExecuteOrder;
            this.eventManager.InvokeForceBlockerResubscribe();
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (this.ignoreNextOrder)
                {
                    this.ignoreNextOrder = false;
                    return;
                }

                if (!args.Process || args.IsQueued || this.recoveryKey)
                {
                    return;
                }

                var isPlayerInput = args.IsPlayerInput;

                if (this.orderSync.ForceNextOrderManual)
                {
                    isPlayerInput = true;
                    this.orderSync.ForceNextOrderManual = false;
                }

                if (this.manualOnly && !isPlayerInput)
                {
                    return;
                }

                if (!args.Entities.Contains(this.owner))
                {
                    return;
                }

                switch (args.OrderId)
                {
                    case OrderId.Ability:
                    {
                        if (args.Ability.Id == AbilityId.item_power_treads)
                        {
                            return;
                        }

                        if (this.switchingThreads)
                        {
                            args.Process = false;
                            return;
                        }

                        var ability = EntityManager9.GetAbility(args.Ability.Handle);
                        if (ability == null)
                        {
                            return;
                        }

                        if (this.PowerTreadsSwitched(ability, false, isPlayerInput))
                        {
                            args.Process = false;
                        }

                        break;
                    }
                    case OrderId.AbilityLocation:
                    {
                        if (this.switchingThreads)
                        {
                            args.Process = false;
                            return;
                        }

                        var ability = EntityManager9.GetAbility(args.Ability.Handle);
                        if (ability == null)
                        {
                            return;
                        }

                        if (this.PowerTreadsSwitched(ability, args.TargetPosition, isPlayerInput))
                        {
                            args.Process = false;
                        }

                        break;
                    }
                    case OrderId.AbilityTarget:
                    {
                        if (this.switchingThreads)
                        {
                            args.Process = false;
                            return;
                        }

                        var ability = EntityManager9.GetAbility(args.Ability.Handle);
                        if (ability == null)
                        {
                            return;
                        }

                        if (this.PowerTreadsSwitched(ability, (Unit)args.Target, isPlayerInput))
                        {
                            args.Process = false;
                        }

                        break;
                    }
                    case OrderId.ToggleAbility:
                    {
                        if (args.Ability.IsToggled)
                        {
                            return;
                        }

                        if (this.switchingThreads)
                        {
                            args.Process = false;
                            return;
                        }

                        var ability = EntityManager9.GetAbility(args.Ability.Handle);
                        if (ability == null)
                        {
                            return;
                        }

                        if (this.PowerTreadsSwitched(ability, true, isPlayerInput))
                        {
                            args.Process = false;
                        }

                        break;
                    }
                    case OrderId.AbilityTargetRune:
                    {
                        if (this.switchingThreads)
                        {
                            args.Process = false;
                            return;
                        }

                        var ability = EntityManager9.GetAbility(args.Ability.Handle);
                        if (ability == null)
                        {
                            return;
                        }

                        if (this.PowerTreadsSwitched(ability, (Rune)args.Target, isPlayerInput))
                        {
                            args.Process = false;
                        }

                        break;
                    }
                    //case OrderId.AbilityTargetTree:
                    //{
                    //    break;
                    //}
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnExecutePowerTreadsOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!args.Process || !args.IsPlayerInput)
                {
                    return;
                }

                if (args.OrderId != OrderId.Ability || args.Ability.Handle != this.powerTreads.Handle)
                {
                    return;
                }

                if (!args.Entities.Contains(this.owner))
                {
                    return;
                }

                if (!this.powerTreads.CanBeCasted())
                {
                    return;
                }

                this.powerTreads.UseAbility();
                this.defaultAttribute = this.powerTreads.ActiveAttribute;
                args.Process = false;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdate()
        {
            try
            {
                if (Game.IsPaused || this.changeBackSleeper || this.switchingThreads || this.recoveryKey)
                {
                    return;
                }

                if (!this.powerTreads.IsValid || this.powerTreads.ActiveAttribute == this.defaultAttribute
                                              || !this.powerTreads.CanBeCasted())
                {
                    return;
                }

                if ((this.powerTreads.Owner.IsCasting || this.powerTreads.Owner.IsInvulnerable || this.powerTreads.Owner.IsInvisible)
                    && !this.forceChangeBackSleeper)
                {
                    return;
                }

                this.powerTreads.UseAbility();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private bool PowerTreadsSwitched(Ability9 ability, Unit target, bool isPlayerInput)
        {
            if (!this.ShouldChangePowerTreads(ability))
            {
                return false;
            }

            var switchCount = this.GetPowerTreadsSwitchCount(ability, target.Position);
            if (switchCount == 0)
            {
                return false;
            }

            this.switchingThreads = true;

            UpdateManager.BeginInvoke(
                async () =>
                    {
                        try
                        {
                            await this.SwitchPowerTreads(switchCount);

                            if (isPlayerInput)
                            {
                                this.ignoreNextOrder = true;
                                this.orderSync.IgnoreSoulRingOrder = true;
                                ability.BaseAbility.UseAbility(target);
                                this.SetSleep(ability, target.Position);
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });

            if (!this.changeBackSleeper)
            {
                this.changeBackSleeper.Sleep(0.5f);
            }

            return true;
        }

        private bool PowerTreadsSwitched(Ability9 ability, Vector3 position, bool isPlayerInput)
        {
            if (!this.ShouldChangePowerTreads(ability))
            {
                return false;
            }

            var switchCount = this.GetPowerTreadsSwitchCount(ability, position);
            if (switchCount == 0)
            {
                return false;
            }

            this.switchingThreads = true;

            UpdateManager.BeginInvoke(
                async () =>
                    {
                        try
                        {
                            await this.SwitchPowerTreads(switchCount);

                            if (isPlayerInput)
                            {
                                this.ignoreNextOrder = true;
                                this.orderSync.IgnoreSoulRingOrder = true;
                                ability.BaseAbility.UseAbility(position);
                                this.SetSleep(ability, position);
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });

            if (!this.changeBackSleeper)
            {
                this.changeBackSleeper.Sleep(0.5f);
            }

            return true;
        }

        private bool PowerTreadsSwitched(Ability9 ability, Rune target, bool isPlayerInput)
        {
            if (!this.ShouldChangePowerTreads(ability))
            {
                return false;
            }

            var switchCount = this.GetPowerTreadsSwitchCount(ability, target.Position);
            if (switchCount == 0)
            {
                return false;
            }

            this.switchingThreads = true;

            UpdateManager.BeginInvoke(
                async () =>
                    {
                        try
                        {
                            await this.SwitchPowerTreads(switchCount);

                            if (isPlayerInput)
                            {
                                this.ignoreNextOrder = true;
                                this.orderSync.IgnoreSoulRingOrder = true;
                                ability.BaseAbility.UseAbility(target);
                                this.SetSleep(ability, target.Position);
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });

            if (!this.changeBackSleeper)
            {
                this.changeBackSleeper.Sleep(0.5f);
            }

            return true;
        }

        private bool PowerTreadsSwitched(Ability9 ability, bool toggle, bool isPlayerInput)
        {
            if (!this.ShouldChangePowerTreads(ability))
            {
                return false;
            }

            var switchCount = this.GetPowerTreadsSwitchCount(ability, Vector3.Zero);
            if (switchCount == 0)
            {
                return false;
            }

            this.switchingThreads = true;

            UpdateManager.BeginInvoke(
                async () =>
                    {
                        try
                        {
                            await this.SwitchPowerTreads(switchCount);

                            if (isPlayerInput)
                            {
                                this.ignoreNextOrder = true;
                                this.orderSync.IgnoreSoulRingOrder = true;

                                if (toggle)
                                {
                                    ability.BaseAbility.ToggleAbility();
                                }
                                else
                                {
                                    ability.BaseAbility.UseAbility();
                                }

                                this.SetSleep(ability, Vector3.Zero);
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });

            if (!this.changeBackSleeper)
            {
                this.changeBackSleeper.Sleep(0.5f);
            }

            return true;
        }

        private void SetSleep(Ability9 ability, Vector3 position)
        {
            if (this.forceChangeBackAbilities.Contains(ability.Id))
            {
                this.forceChangeBackSleeper.Sleep(0.3f);
                this.changeBackSleeper.Sleep(ability.IsItem ? 0.01f : 0.1f);
                return;
            }

            var delay = ability.CastPoint;

            if (!position.IsZero)
            {
                delay += ability.Owner.GetTurnTime(position);
            }

            this.changeBackSleeper.Sleep(delay + 0.5f);
        }

        private bool ShouldChangePowerTreads(Ability ability)
        {
            if (!this.powerTreads.IsValid)
            {
                return false;
            }

            if (ability.ManaCost <= 0)
            {
                return false;
            }

            if (!this.powerTreads.CanBeCasted(false) || this.powerTreads.Owner.IsInvulnerable || this.powerTreads.Owner.IsInvisible)
            {
                return false;
            }

            return true;
        }

        private async Task SwitchPowerTreads(int count)
        {
            this.switchingThreads = true;

            this.powerTreads.ChangeExpectedAttribute(count == 1);

            for (var i = 0; i < count; i++)
            {
                this.powerTreads.UseAbilitySimple();
                await Task.Delay(Delay);
            }

            this.switchingThreads = false;
        }
    }
}