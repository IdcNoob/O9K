namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;

    using Core.Entities.Metadata;
    using Core.Logger;
    using Core.Managers.Context;

    using Ensage;

    using Helpers.Notificator;

    using MainMenu;

    [AbilityId(AbilityId.tusk_snowball)]
    internal class Snowball : AbilityModule
    {
        private ParticleEffect effect;

        public Snowball(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
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

                if (args.Modifier.Name != "modifier_tusk_snowball_target")
                {
                    return;
                }

                this.effect = new ParticleEffect(
                    "particles/units/heroes/hero_tusk/tusk_snowball_target.vpcf",
                    sender,
                    ParticleAttachment.OverheadFollow);

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

                if (args.Modifier.Name != "modifier_tusk_snowball_target")
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