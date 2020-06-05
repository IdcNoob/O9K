namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.RemoteMines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base;

    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;

    using Helpers.Notificator;

    internal class RemoteMinesAbilityData : AbilityFullData
    {
        public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, Unit unit, INotificator notificator)
        {
            var mine = drawableAbilities.OfType<DrawableRemoteMinesAbility>()
                .FirstOrDefault(x => x.Unit == null && x.Position.Distance2D(unit.Position) < 10);

            if (mine != null)
            {
                mine.AddUnit(unit);
                return;
            }

            var owner = unit.Owner;
            var ownerName = owner.Name;

            var drawableAbility = new DrawableUnitAbility
            {
                AbilityTexture = this.AbilityId + "_rounded",
                AbilityId = this.AbilityId,
                HeroTexture = ownerName + "_rounded",
                MinimapHeroTexture = ownerName + "_icon",
                Position = unit.Position,
                Unit = unit,
                IsShowingRange = this.ShowRange,
                Range = this.Range,
                RangeColor = this.RangeColor,
                Duration = this.Duration,
                ShowUntil = Game.RawGameTime + this.Duration,
                ShowHeroUntil = Game.RawGameTime + this.TimeToShow,
                Owner = owner
            };

            drawableAbility.DrawRange();
            drawableAbilities.Add(drawableAbility);
        }

        public override void AddDrawableAbility(
            List<IDrawableAbility> drawableAbilities,
            ParticleEffect particle,
            Team allyTeam,
            INotificator notificator)
        {
            var position = particle.GetControlPoint(this.ControlPoint);

            if (particle.Name.Contains("detonate"))
            {
                var mine = drawableAbilities.OfType<DrawableRemoteMinesAbility>().FirstOrDefault(x => x.Position.Distance2D(position) < 10);
                if (mine != null)
                {
                    if (mine.IsShowingRange)
                    {
                        mine.RemoveRange();
                    }

                    drawableAbilities.Remove(mine);
                }
            }
            else
            {
                var mine = ObjectManager.GetEntitiesFast<Unit>()
                    .FirstOrDefault(x => x.Name == "npc_dota_techies_remote_mine" && x.Distance2D(position) < 10);

                if (mine != null)
                {
                    return;
                }

                var owner = EntityManager9.GetUnit(particle.Owner.Handle);
                if (owner == null)
                {
                    return;
                }

                UpdateManager.BeginInvoke(
                    () =>
                        {
                            try
                            {
                                if (!particle.IsValid)
                                {
                                    return;
                                }

                                var drawableAbility = new DrawableRemoteMinesAbility
                                {
                                    AbilityTexture = this.AbilityId + "_rounded",
                                    AbilityId = this.AbilityId,
                                    HeroTexture = owner.Name + "_rounded",
                                    MinimapHeroTexture = owner.Name + "_icon",
                                    Position = position.SetZ(350),
                                    Duration = this.Duration,
                                    IsShowingRange = this.ShowRange,
                                    Range = this.Range,
                                    RangeColor = this.RangeColor,
                                    ShowUntil = Game.RawGameTime + this.Duration,
                                    ShowHeroUntil = Game.RawGameTime + this.TimeToShow,
                                    BaseEntity = owner.BaseEntity
                                };

                                owner.ChangeBasePosition(drawableAbility.Position);

                                drawableAbility.DrawRange();
                                drawableAbilities.Add(drawableAbility);
                            }
                            catch (Exception e)
                            {
                                Logger.Error(e);
                            }
                        },
                    1000);
            }
        }
    }
}