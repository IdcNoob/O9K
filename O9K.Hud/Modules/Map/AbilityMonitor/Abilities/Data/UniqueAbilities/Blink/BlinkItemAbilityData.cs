namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Blink
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;

    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Helpers.Notificator;

    internal class BlinkItemAbilityData : AbilityFullData
    {
        public override void AddDrawableAbility(
            List<IDrawableAbility> drawableAbilities,
            ParticleEffect particle,
            Team allyTeam,
            INotificator notificator)
        {
            var position = particle.GetControlPoint(this.ControlPoint);
            var blinks = EntityManager9.Abilities.Where(
                    x => (x.Id == this.AbilityId || x.Id == AbilityId.item_flicker) && x.Owner.CanUseAbilities && x.Owner.IsAlive)
                .ToList();

            if (blinks.Count == 0 || blinks.All(x => x.Owner.Team == allyTeam))
            {
                return;
            }

            if (blinks.Any(x => x.Owner.IsVisible && x.Owner.Distance(position) < 1500 && x.TimeSinceCasted < 0.5f))
            {
                return;
            }

            var enemyBlinks = blinks.Where(x => x.Owner.Team != allyTeam && x.RemainingCooldown <= 0).ToList();

            if (enemyBlinks.Count == 0)
            {
                return;
            }

            if (enemyBlinks.Count == 1)
            {
                var owner = enemyBlinks[0].Owner;
                var drawableAbility = new DrawableAbility
                {
                    AbilityTexture = this.AbilityId + "_rounded",
                    HeroTexture = owner.Name + "_rounded",
                    MinimapHeroTexture = owner.Name + "_icon",
                    ShowUntil = Game.RawGameTime + this.TimeToShow,
                    Position = Game.RawGameTime - owner.LastVisibleTime < 0.5f
                                   ? owner.InFront(enemyBlinks[0].Range - 200)
                                   : position.SetZ(350)
                };

                owner.ChangeBasePosition(drawableAbility.Position);
                drawableAbilities.Add(drawableAbility);
            }
            else
            {
                var owner = enemyBlinks.Find(x => x.Owner.Distance(position) < 100 && Game.RawGameTime - x.Owner.LastVisibleTime < 0.5f)
                    ?.Owner;

                if (owner != null)
                {
                    var drawableAbility = new DrawableAbility
                    {
                        AbilityTexture = this.AbilityId + "_rounded",
                        HeroTexture = owner.Name + "_rounded",
                        MinimapHeroTexture = owner.Name + "_icon",
                        ShowUntil = Game.RawGameTime + this.TimeToShow,
                        Position = owner.InFront(enemyBlinks[0].Range - 200)
                    };

                    owner.ChangeBasePosition(drawableAbility.Position);
                    drawableAbilities.Add(drawableAbility);
                }
                else
                {
                    var drawableAbility = new SimpleDrawableAbility
                    {
                        AbilityTexture = this.AbilityId + "_rounded",
                        ShowUntil = Game.RawGameTime + this.TimeToShow,
                        Position = (this.RawParticlePosition ? particle.Position : particle.GetControlPoint(this.ControlPoint)).SetZ(350)
                    };

                    drawableAbilities.Add(drawableAbility);
                }
            }
        }
    }
}