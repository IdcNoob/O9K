namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.FireRemnant
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;

    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Helpers.Notificator;

    internal class FireRemnantAbilityData : AbilityFullData
    {
        public uint StartControlPoint { get; set; } = 0;

        public override void AddDrawableAbility(
            List<IDrawableAbility> drawableAbilities,
            ParticleEffect particle,
            Team allyTeam,
            INotificator notificator)
        {
            if (particle.Name.Contains("dash"))
            {
                drawableAbilities.RemoveAll(x => x is DrawableFireRemnantAbility);
            }
            else
            {
                var owner = EntityManager9.GetUnit(particle.Owner.Owner.Handle);
                if (owner == null)
                {
                    return;
                }

                var startPosition = particle.GetControlPoint(this.StartControlPoint);

                if (!owner.IsVisible)
                {
                    var drawableAbilityStart = new DrawableAbility
                    {
                        AbilityTexture = this.AbilityId + "_rounded",
                        HeroTexture = owner.Name + "_rounded",
                        MinimapHeroTexture = owner.Name + "_icon",
                        ShowUntil = Game.RawGameTime + this.TimeToShow,
                        Position = startPosition.SetZ(350)
                    };

                    owner.ChangeBasePosition(drawableAbilityStart.Position);
                    drawableAbilities.Add(drawableAbilityStart);
                }

                var remnants = drawableAbilities.OfType<DrawableFireRemnantAbility>().ToArray();
                var unit = ObjectManager.GetEntitiesFast<Unit>()
                    .Concat(ObjectManager.GetDormantEntities<Unit>())
                    .FirstOrDefault(
                        x => x.IsAlive && x.Name == "npc_dota_ember_spirit_remnant" && x.Distance2D(startPosition) < 1500
                             && remnants.All(z => z.Unit != x));

                if (unit == null)
                {
                    return;
                }

                var drawableAbility = new DrawableFireRemnantAbility
                {
                    AbilityTexture = this.AbilityId + "_rounded",
                    HeroTexture = owner.Name + "_rounded",
                    MinimapHeroTexture = owner.Name + "_icon",
                    Position = particle.GetControlPoint(this.ControlPoint).SetZ(350),
                    Duration = this.Duration,
                    ShowUntil = Game.RawGameTime + this.Duration,
                    ShowHeroUntil = Game.RawGameTime + this.TimeToShow,
                    Owner = owner.BaseEntity,
                    Unit = unit
                };

                drawableAbilities.Add(drawableAbility);
            }
        }
    }
}