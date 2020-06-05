namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;
    using System.Collections.Generic;

    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;

    using Ensage;

    using Helpers.Notificator;

    using MainMenu;

    using SharpDX;

    [AbilityId(AbilityId.treant_eyes_in_the_forest)]
    internal class EyesInTheForest : AbilityModule
    {
        private readonly Dictionary<uint, ParticleEffect> effects = new Dictionary<uint, ParticleEffect>();

        private readonly Vector3 radius;

        public EyesInTheForest(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
            var radiusData = new SpecialData(AbilityId.treant_eyes_in_the_forest, "vision_aoe").GetValue(1);
            this.radius = new Vector3(radiusData, -radiusData, -radiusData);
        }

        protected override void Disable()
        {
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            EntityManager9.UnitMonitor.UnitDied -= this.OnUnitRemoved;
        }

        protected override void Enable()
        {
            EntityManager9.UnitAdded += this.OnUnitAdded;
            EntityManager9.UnitRemoved += this.OnUnitRemoved;
            EntityManager9.UnitMonitor.UnitDied += this.OnUnitRemoved;
        }

        private void OnUnitAdded(Unit9 unit)
        {
            try
            {
                if (unit.Team == this.OwnerTeam || unit.Name != "npc_dota_treant_eyes")
                {
                    return;
                }

                var effect = new ParticleEffect("particles/units/heroes/hero_treant/treant_eyesintheforest.vpcf", unit.Position);
                effect.SetControlPoint(1, this.radius);
                this.effects.Add(unit.Handle, effect);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitRemoved(Unit9 unit)
        {
            try
            {
                if (unit.Team == this.OwnerTeam)
                {
                    return;
                }

                if (!this.effects.TryGetValue(unit.Handle, out var effect))
                {
                    return;
                }

                effect.Dispose();
                this.effects.Remove(unit.Handle);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}