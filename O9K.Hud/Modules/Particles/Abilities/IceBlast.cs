namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;

    using Core.Data;
    using Core.Entities.Metadata;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Particle;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Helpers.Notificator;

    using MainMenu;

    using SharpDX;

    [AbilityId(AbilityId.ancient_apparition_ice_blast)]
    internal class IceBlast : AbilityModule
    {
        private readonly float growRadius;

        private readonly float maxRadius;

        private readonly float minRadius;

        private readonly float speed;

        private ParticleEffect effect;

        private float unitAddTime;

        public IceBlast(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
            this.minRadius = new SpecialData(AbilityId.ancient_apparition_ice_blast, "radius_min").GetValue(1);
            this.maxRadius = new SpecialData(AbilityId.ancient_apparition_ice_blast, "radius_max").GetValue(1);
            this.growRadius = new SpecialData(AbilityId.ancient_apparition_ice_blast, "radius_grow").GetValue(1);
            this.speed = new SpecialData(AbilityId.ancient_apparition_ice_blast, "speed").GetValue(1);

            //todo add notification
            //this.notificationsEnabled = hudMenu.NotificationsMenu.GetOrAdd(new Menu("Abilities"))
            //    .GetOrAdd(new Menu("Used"))
            //    .GetOrAdd(new MenuSwitcher("Enabled"));
        }

        protected override void Disable()
        {
            ObjectManager.OnAddEntity -= this.OnAddEntity;
            this.Context.ParticleManger.ParticleAdded -= this.OnParticleAdded;
        }

        protected override void Enable()
        {
            ObjectManager.OnAddEntity += this.OnAddEntity;
        }

        private void OnAddEntity(EntityEventArgs args)
        {
            try
            {
                var unit = args.Entity as Unit;
                if (unit == null || unit.Team == this.OwnerTeam || unit.UnitType != 0 || unit.NetworkName != "CDOTA_BaseNPC")
                {
                    return;
                }

                if (unit.DayVision != GameData.AbilityVision[AbilityId.ancient_apparition_ice_blast])
                {
                    return;
                }

                this.unitAddTime = Game.RawGameTime;
                this.Context.ParticleManger.ParticleAdded += this.OnParticleAdded;

                //if (!this.notificationsEnabled)
                //{
                //    return;
                //}

                //this.notificator.PushNotification(
                //    new AbilityNotification(
                //        nameof(HeroId.npc_dota_hero_ancient_apparition),
                //        nameof(AbilityId.ancient_apparition_ice_blast)));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnParticleAdded(Particle particle)
        {
            try
            {
                if (!particle.Released)
                {
                    return;
                }

                switch (particle.Name)
                {
                    case "particles/units/heroes/hero_ancient_apparition/ancient_apparition_ice_blast_final.vpcf":
                    case "particles/econ/items/ancient_apparition/aa_blast_ti_5/ancient_apparition_ice_blast_final_ti5.vpcf":
                    {
                        var startPosition = particle.GetControlPoint(0);
                        var endPosition = particle.GetControlPoint(1);
                        var time = Game.RawGameTime - (Game.Ping / 1000);
                        var flyingTime = time - this.unitAddTime;
                        var direction = startPosition + endPosition;
                        var position = startPosition.Extend2D(direction, flyingTime * this.speed);
                        var radius = Math.Min(this.maxRadius, Math.Max((flyingTime * this.growRadius) + this.minRadius, this.minRadius));

                        this.effect = new ParticleEffect(
                            "particles/units/heroes/hero_ancient_apparition/ancient_apparition_ice_blast_marker.vpcf",
                            position.SetZ(384)); // todo correct z coord
                        this.effect.SetControlPoint(1, new Vector3(radius, 1, 1));
                        break;
                    }
                    case "particles/econ/items/ancient_apparition/aa_blast_ti_5/ancient_apparition_ice_blast_explode_ti5.vpcf":
                    case "particles/units/heroes/hero_ancient_apparition/ancient_apparition_ice_blast_explode.vpcf":
                        this.Context.ParticleManger.ParticleAdded -= this.OnParticleAdded;
                        this.effect?.Dispose();
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}