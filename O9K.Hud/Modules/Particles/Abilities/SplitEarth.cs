namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;

    using Core.Entities.Metadata;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;

    using Ensage;

    using Helpers.Notificator;

    using MainMenu;

    using SharpDX;

    [AbilityId(AbilityId.leshrac_split_earth)]
    internal class SplitEarth : AbilityModule
    {
        private readonly Vector3 radius;

        public SplitEarth(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
            var radiusData = new SpecialData(AbilityId.leshrac_split_earth, "radius").GetValue(3);
            this.radius = new Vector3(radiusData, -radiusData, -radiusData);
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

                if (args.Modifier.Name != "modifier_leshrac_split_earth_thinker")
                {
                    return;
                }

                var effect = new ParticleEffect("particles/units/heroes/hero_leshrac/leshrac_split_projected.vpcf", sender.Position);
                effect.SetControlPoint(1, this.radius);

                effect.Release();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}