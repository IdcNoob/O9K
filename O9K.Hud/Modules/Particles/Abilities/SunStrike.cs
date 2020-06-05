namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;

    using Core.Entities.Metadata;
    using Core.Logger;
    using Core.Managers.Context;

    using Ensage;

    using Helpers.Notificator;

    using MainMenu;

    [AbilityId(AbilityId.invoker_sun_strike)]
    internal class SunStrike : AbilityModule
    {
        public SunStrike(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
        }

        protected override void Disable()
        {
            Unit.OnModifierAdded -= this.OnModifierAdded;
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

                if (args.Modifier.Name != "modifier_invoker_sun_strike")
                {
                    return;
                }

                var effect = new ParticleEffect(
                    "particles/econ/items/invoker/invoker_apex/invoker_sun_strike_team_immortal1.vpcf",
                    sender.Position);
                effect.Release();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}