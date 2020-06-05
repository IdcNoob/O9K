namespace O9K.Hud.Modules.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Helpers.Notificator;
    using Helpers.Notificator.Notifications;

    using MainMenu;

    internal class Items : IHudModule
    {
        private readonly MenuSwitcher enabled;

        private readonly MenuAbilityToggler force;

        private readonly MenuSlider goldThreshold;

        private readonly INotificator notificator;

        private readonly MenuSwitcher ping;

        private HashSet<uint> ignoredItems;

        private Team ownerTeam;

        [ImportingConstructor]
        public Items(IHudMenu hudMenu, INotificator notificator)
        {
            this.notificator = notificator;

            var menu = hudMenu.NotificationsMenu.Add(new Menu("Items"));
            menu.AddTranslation(Lang.Ru, "Предметы");
            menu.AddTranslation(Lang.Cn, "物品");

            this.enabled = menu.Add(new MenuSwitcher("Enabled")).SetTooltip("Notify about enemy item purchases");
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Оповещать о купленых предметах");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "通知有关敌方物品的购买");

            this.ping = menu.Add(new MenuSwitcher("Ping on click", false)).SetTooltip("Ping item to allies");
            this.ping.AddTranslation(Lang.Ru, "Оповещать при нажатии");
            this.ping.AddTooltipTranslation(Lang.Ru, "Оповещать союзников при нажатии");
            this.ping.AddTranslation(Lang.Cn, "单击时发出警报");
            this.ping.AddTooltipTranslation(Lang.Cn, "按下時通知盟友");

            this.goldThreshold = menu.Add(new MenuSlider("Item cost", 2000, 500, 6000)).SetTooltip("Item cost threshold");
            this.goldThreshold.AddTranslation(Lang.Ru, "Цена предмета");
            this.goldThreshold.AddTooltipTranslation(Lang.Ru, "Оповещать если цена предмета выше");
            this.goldThreshold.AddTranslation(Lang.Cn, "物品项目成本");
            this.goldThreshold.AddTooltipTranslation(Lang.Cn, "物品成本门槛");

            this.force = menu.Add(
                new MenuAbilityToggler(
                    "Always show:",
                    new Dictionary<AbilityId, bool>
                    {
                        { AbilityId.item_ward_observer, true },
                        { AbilityId.item_ward_sentry, true },
                        { AbilityId.item_ward_dispenser, true },
                        { AbilityId.item_dust, true },
                        { AbilityId.item_gem, true },
                        { AbilityId.item_smoke_of_deceit, true },
                    }));
            this.force.AddTranslation(Lang.Ru, "Оповещать всегда:");
            this.force.AddTranslation(Lang.Cn, "总是显示");
        }

        public void Activate()
        {
            this.ownerTeam = EntityManager9.Owner.Team;
            this.ignoredItems = EntityManager9.Abilities.Where(x => x.IsItem && x.Owner.Team != this.ownerTeam)
                .Select(x => x.Handle)
                .ToHashSet();
            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.AbilityAdded += this.OnAbilityAdded;
            }
            else
            {
                EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            }
        }

        private bool IsItemEnabled(Ability9 ability)
        {
            if (this.force.IsEnabled(ability.Name))
            {
                return true;
            }

            if (ability.BaseItem.Cost >= this.goldThreshold)
            {
                return true;
            }

            var tier = ability.BaseAbility.NeutralTierIndex + 1;
            if (tier > 0 && tier * 1000 >= this.goldThreshold)
            {
                return true;
            }

            return false;
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!ability.IsItem || !ability.Owner.IsHero || ability.Owner.IsIllusion || ability.Owner.Team == this.ownerTeam)
                {
                    return;
                }

                if (this.ignoredItems.Contains(ability.Handle) || ability.BaseItem.IsRecipe)
                {
                    return;
                }

                if (!this.IsItemEnabled(ability))
                {
                    return;
                }

                this.notificator.PushNotification(new PurchaseNotification(ability, this.ping));
                this.ignoredItems.Add(ability.Handle);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}