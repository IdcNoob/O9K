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
    using Ensage.SDK.Helpers;

    using Helpers.Notificator;
    using Helpers.Notificator.Notifications;

    using MainMenu;

    internal class StolenAbility : IHudModule
    {
        private readonly MenuSwitcher enabled;

        private readonly HashSet<uint> ignored = new HashSet<uint>();

        private readonly INotificator notificator;

        private readonly MenuSwitcher ping;

        private Team ownerTeam;

        [ImportingConstructor]
        public StolenAbility(IHudMenu hudMenu, INotificator notificator)
        {
            this.notificator = notificator;

            var notificationMenu = hudMenu.NotificationsMenu.GetOrAdd(new Menu("Abilities"));
            notificationMenu.AddTranslation(Lang.Ru, "Способности");
            notificationMenu.AddTranslation(Lang.Cn, "播放声音");

            var menu = notificationMenu.Add(new Menu("Stolen"));
            menu.AddTranslation(Lang.Ru, "Украденные");
            menu.AddTranslation(Lang.Cn, "盗取");

            this.enabled = menu.Add(new MenuSwitcher("Enabled")).SetTooltip("Notify about rubick's stolen abilities");
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Оповещать о украденных способностях Рубика");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "通知被" + LocalizationHelper.LocalizeName(HeroId.npc_dota_hero_rubick) + "偷的技能");

            this.ping = menu.Add(new MenuSwitcher("Ping on click")).SetTooltip("Ping ability to allies");
            this.ping.AddTranslation(Lang.Ru, "Оповещать при нажатии");
            this.ping.AddTooltipTranslation(Lang.Ru, "Оповещать союзников при нажатии");
            this.ping.AddTranslation(Lang.Cn, "单击时发出警报");
            this.ping.AddTooltipTranslation(Lang.Cn, "按下時通知盟友");
        }

        public void Activate()
        {
            this.ownerTeam = EntityManager9.Owner.Team;

            foreach (var ability in EntityManager9.Abilities.Where(x => x.IsStolen))
            {
                this.ignored.Add(ability.Handle);
            }

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

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!ability.IsStolen || ability.Owner.Team == this.ownerTeam || !ability.IsUsable)
                {
                    return;
                }

                if (this.ignored.Contains(ability.Handle))
                {
                    return;
                }

                this.notificator.PushNotification(new StolenAbilityNotification(ability, this.ping));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}