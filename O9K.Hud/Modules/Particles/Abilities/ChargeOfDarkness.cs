namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;

    using Core.Entities.Metadata;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Menu.Items;

    using Ensage;

    using Helpers.Notificator;
    using Helpers.Notificator.Notifications;

    using MainMenu;

    [AbilityId(AbilityId.spirit_breaker_charge_of_darkness)]
    internal class ChargeOfDarkness : AbilityModule
    {
        private readonly MenuSwitcher notificationsEnabled;

        private ParticleEffect effect;

        public ChargeOfDarkness(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
            this.notificationsEnabled = hudMenu.NotificationsMenu.GetOrAdd(new Menu("Abilities"))
                .GetOrAdd(new Menu("Used"))
                .GetOrAdd(new MenuSwitcher("Enabled"));
        }

        protected override void Disable()
        {
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
        }

        protected override void Enable()
        {
            Unit.OnModifierAdded += this.OnModifierAdded;
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Team != this.OwnerTeam)
                {
                    return;
                }

                if (args.Modifier.Name != "modifier_spirit_breaker_charge_of_darkness_vision")
                {
                    return;
                }

                this.effect = new ParticleEffect(
                    "particles/units/heroes/hero_spirit_breaker/spirit_breaker_charge_target.vpcf",
                    sender,
                    ParticleAttachment.OverheadFollow);

                if (this.notificationsEnabled && sender is Hero)
                {
                    this.Notificator.PushNotification(
                        new AbilityHeroNotification(
                            nameof(HeroId.npc_dota_hero_spirit_breaker),
                            nameof(AbilityId.spirit_breaker_charge_of_darkness),
                            sender.Name));
                }

                Unit.OnModifierRemoved += this.OnModifierRemoved;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (sender.Team != this.OwnerTeam)
                {
                    return;
                }

                if (args.Modifier.Name != "modifier_spirit_breaker_charge_of_darkness_vision")
                {
                    return;
                }

                this.effect.Dispose();
                Unit.OnModifierRemoved -= this.OnModifierRemoved;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}