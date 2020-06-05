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

    [AbilityId(AbilityId.life_stealer_infest)]
    internal class Infest : AbilityModule
    {
        private readonly MenuSwitcher notificationsEnabled;

        private ParticleEffect effect;

        public Infest(IContext9 context, INotificator notificator, IHudMenu hudMenu)
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
                if (sender.Team == this.OwnerTeam)
                {
                    return;
                }

                if (args.Modifier.Name != "modifier_life_stealer_infest_effect")
                {
                    return;
                }

                this.effect = new ParticleEffect(
                    "particles/units/heroes/hero_life_stealer/life_stealer_infested_unit.vpcf",
                    sender,
                    ParticleAttachment.OverheadFollow);

                if (this.notificationsEnabled && sender is Hero)
                {
                    this.Notificator.PushNotification(
                        new AbilityHeroNotification(
                            nameof(HeroId.npc_dota_hero_life_stealer),
                            nameof(AbilityId.life_stealer_infest),
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
                if (sender.Team == this.OwnerTeam)
                {
                    return;
                }

                if (args.Modifier.Name != "modifier_life_stealer_infest_effect")
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