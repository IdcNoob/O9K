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

    [AbilityId(AbilityId.lina_light_strike_array)]
    internal class LightStrikeArray : AbilityModule
    {
        private readonly Vector3 radius;

        public LightStrikeArray(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
            var radiusData = new SpecialData(AbilityId.lina_light_strike_array, "light_strike_array_aoe").GetValue(3);
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

                if (args.Modifier.Name != "modifier_lina_light_strike_array")
                {
                    return;
                }

                var effect = new ParticleEffect("particles/econ/items/lina/lina_ti7/light_strike_array_pre_ti7.vpcf", sender.Position);
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