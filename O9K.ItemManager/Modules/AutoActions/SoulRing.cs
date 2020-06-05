namespace O9K.ItemManager.Modules.AutoActions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
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

    internal class SoulRing : IModule
    {
        private const int Delay = 50;

        private readonly MenuSwitcher enabled;

        private readonly IAssemblyEventManager9 eventManager;

        private readonly MenuSlider hpThreshold;

        private readonly HashSet<AbilityId> ignoredAbilities = new HashSet<AbilityId>
        {
            AbilityId.item_tpscroll,
            AbilityId.item_travel_boots,
            AbilityId.item_travel_boots_2
        };

        private readonly MenuSwitcher manualOnly;

        private readonly IOrderSync orderSync;

        private readonly MenuHoldKey recoveryKey;

        private readonly MenuAbilityToggler toggler;

        private bool ignoreNextOrder;

        private Owner owner;

        private ActiveAbility soulRing;

        private bool subscribed;

        [ImportingConstructor]
        public SoulRing(IMainMenu mainMenu, IAssemblyEventManager9 eventManager, IOrderSync orderSync)
        {
            this.eventManager = eventManager;
            this.orderSync = orderSync;

            var menu = mainMenu.AutoActionsMenu.Add(new Menu(LocalizationHelper.LocalizeName(AbilityId.item_soul_ring), "SoulRing"));

            this.enabled = menu.Add(new MenuSwitcher("Enabled"));
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTranslation(Lang.Cn, "启用");

            this.manualOnly = menu.Add(new MenuSwitcher("Manual only", false).SetTooltip("Use only when abilities are casted manually"));
            this.manualOnly.AddTranslation(Lang.Ru, "Только вручную");
            this.manualOnly.AddTranslation(Lang.Cn, "仅手册");
            this.manualOnly.AddTooltipTranslation(Lang.Ru, "Использовать только когда способности кастуются вручную");
            this.manualOnly.AddTooltipTranslation(Lang.Cn, "仅在手动使用技能时使用");

            this.toggler = menu.Add(new MenuAbilityToggler("Abilities"));
            this.toggler.AddTranslation(Lang.Ru, "Способности");
            this.toggler.AddTranslation(Lang.Cn, "播放声音");

            this.hpThreshold = menu.Add(new MenuSlider("Health%", 30, 0, 100));
            this.hpThreshold.AddTranslation(Lang.Ru, "Здоровье%");
            this.hpThreshold.AddTranslation(Lang.Cn, "生命值％");

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
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            this.subscribed = false;
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.AbilityAdded += this.OnAbilityAdded;
                EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
            }
            else
            {
                EntityManager9.AbilityAdded -= this.OnAbilityAdded;
                EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
                Player.OnExecuteOrder -= this.OnExecuteOrder;
                this.subscribed = false;
            }
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!ability.Owner.IsMyHero || !(ability is ActiveAbility active))
                {
                    return;
                }

                if (ability.Id == AbilityId.item_soul_ring)
                {
                    if (this.subscribed)
                    {
                        return;
                    }

                    this.soulRing = active;
                    Player.OnExecuteOrder += this.OnExecuteOrder;
                    this.eventManager.InvokeForceBlockerResubscribe();
                    this.eventManager.InvokeAutoSoulRingEnabled();
                    this.subscribed = true;
                }
                else if (ability.BaseAbility.GetManaCost(1) > 0 && !this.ignoredAbilities.Contains(ability.Id))
                {
                    this.toggler.AddAbility(ability.Id);
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

                if (ability.Handle != this.soulRing?.Handle)
                {
                    return;
                }

                Player.OnExecuteOrder -= this.OnExecuteOrder;
                this.subscribed = false;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
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

                if (this.orderSync.IgnoreSoulRingOrder)
                {
                    this.orderSync.IgnoreSoulRingOrder = false;
                    return;
                }

                if (!args.Process || args.IsQueued || this.recoveryKey)
                {
                    return;
                }

                if (this.manualOnly && !args.IsPlayerInput)
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
                        if (args.Ability.Id == AbilityId.item_soul_ring)
                        {
                            return;
                        }

                        if (this.SoulRingUsed(args.Ability, false, args.IsPlayerInput))
                        {
                            args.Process = false;
                        }

                        break;
                    }
                    case OrderId.AbilityLocation:
                    {
                        if (this.SoulRingUsed(args.Ability, args.TargetPosition, args.IsPlayerInput))
                        {
                            args.Process = false;
                        }

                        break;
                    }
                    case OrderId.AbilityTarget:
                    {
                        if (this.SoulRingUsed(args.Ability, (Unit)args.Target, args.IsPlayerInput))
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

                        if (this.SoulRingUsed(args.Ability, true, args.IsPlayerInput))
                        {
                            args.Process = false;
                        }

                        break;
                    }
                    case OrderId.AbilityTargetRune:
                    {
                        if (this.SoulRingUsed(args.Ability, (Rune)args.Target, args.IsPlayerInput))
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

        private bool ShouldUseSoulRing(Ability ability)
        {
            if (!this.soulRing.CanBeCasted(false))
            {
                return false;
            }

            if (ability.ManaCost <= 0 || !this.toggler.IsEnabled(ability.Name))
            {
                return false;
            }

            if (!this.owner.Hero.CanBeHealed || this.owner.Hero.HealthPercentage < this.hpThreshold)
            {
                return false;
            }

            return true;
        }

        private bool SoulRingUsed(Ability ability, Vector3 position, bool isPlayerInput)
        {
            if (!this.ShouldUseSoulRing(ability))
            {
                return false;
            }

            this.soulRing.UseAbility();

            if (isPlayerInput)
            {
                UpdateManager.BeginInvoke(
                    () =>
                        {
                            this.ignoreNextOrder = true;
                            this.orderSync.ForceNextOrderManual = true;
                            ability.UseAbility(position);
                        },
                    Delay);
            }

            return true;
        }

        private bool SoulRingUsed(Ability ability, Unit target, bool isPlayerInput)
        {
            if (!this.ShouldUseSoulRing(ability))
            {
                return false;
            }

            this.soulRing.UseAbility();

            if (isPlayerInput)
            {
                UpdateManager.BeginInvoke(
                    () =>
                        {
                            this.ignoreNextOrder = true;
                            this.orderSync.ForceNextOrderManual = true;
                            ability.UseAbility(target);
                        },
                    Delay);
            }

            return true;
        }

        private bool SoulRingUsed(Ability ability, Rune rune, bool isPlayerInput)
        {
            if (!this.ShouldUseSoulRing(ability))
            {
                return false;
            }

            this.soulRing.UseAbility();

            if (isPlayerInput)
            {
                UpdateManager.BeginInvoke(
                    () =>
                        {
                            this.ignoreNextOrder = true;
                            this.orderSync.ForceNextOrderManual = true;
                            ability.UseAbility(rune);
                        },
                    Delay);
            }

            return true;
        }

        private bool SoulRingUsed(Ability ability, bool toggle, bool isPlayerInput)
        {
            if (!this.ShouldUseSoulRing(ability))
            {
                return false;
            }

            this.soulRing.UseAbility();

            if (isPlayerInput)
            {
                UpdateManager.BeginInvoke(
                    () =>
                        {
                            this.ignoreNextOrder = true;
                            this.orderSync.ForceNextOrderManual = true;

                            if (toggle)
                            {
                                ability.ToggleAbility();
                            }
                            else
                            {
                                ability.UseAbility();
                            }
                        },
                    Delay);
            }

            return true;
        }
    }
}