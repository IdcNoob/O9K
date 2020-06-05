namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;

    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Helpers.Notificator;

    using MainMenu;

    [AbilityId(AbilityId.slark_dark_pact)]
    internal class DarkPact : AbilityModule
    {
        private ParticleEffect effect;

        private IUpdateHandler handler;

        private Unit9 unit;

        public DarkPact(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
        }

        protected override void Disable()
        {
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
            UpdateManager.Unsubscribe(this.handler);
        }

        protected override void Enable()
        {
            this.handler = UpdateManager.Subscribe(this.OnUpdate, 0, false);
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

                if (args.Modifier.Name != "modifier_slark_dark_pact")
                {
                    return;
                }

                this.unit = EntityManager9.GetUnit(sender.Handle);
                if (this.unit == null)
                {
                    return;
                }

                this.effect = new ParticleEffect(
                    "particles/units/heroes/hero_slark/slark_dark_pact_start.vpcf",
                    sender,
                    ParticleAttachment.AbsOriginFollow);

                Unit.OnModifierRemoved += this.OnModifierRemoved;
                this.handler.IsEnabled = true;
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

                if (args.Modifier.Name != "modifier_slark_dark_pact")
                {
                    return;
                }

                this.handler.IsEnabled = false;
                Unit.OnModifierRemoved -= this.OnModifierRemoved;
                this.effect.Dispose();
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
                if (!this.effect.IsValid || !this.unit.IsAlive || !this.unit.IsVisible)
                {
                    this.handler.IsEnabled = false;
                    return;
                }

                this.effect.SetControlPoint(1, this.unit.Position);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}