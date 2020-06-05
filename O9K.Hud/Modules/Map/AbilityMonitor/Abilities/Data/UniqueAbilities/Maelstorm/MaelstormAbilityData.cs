namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Maelstorm
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;

    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Helpers.Notificator;

    internal class MaelstormAbilityData : AbilityFullData
    {
        public override void AddDrawableAbility(
            List<IDrawableAbility> drawableAbilities,
            ParticleEffect particle,
            Team allyTeam,
            INotificator notificator)
        {
            var position = (this.RawParticlePosition ? particle.Position : particle.GetControlPoint(this.ControlPoint)).SetZ(350);
            var owners = EntityManager9.Abilities.Where(x => x.Id == this.AbilityId || x.Id == AbilityId.item_mjollnir)
                .Select(x => x.Owner)
                .Where(x => x.CanUseAbilities && x.IsAlive)
                .ToList();

            if (owners.Any(x => x.IsVisible && x.Distance(position) <= x.GetAttackRange() + 400))
            {
                return;
            }

            var owner = owners.Find(x => x.Team != allyTeam && !x.IsVisible);
            if (owner == null)
            {
                return;
            }

            string ownerName;

            if (owner.IsHero)
            {
                ownerName = owner.Name;
            }
            else
            {
                ownerName = owner.Owner?.Name;

                if (ownerName == null)
                {
                    return;
                }
            }

            var drawableAbility = new DrawableAbility
            {
                AbilityTexture = this.AbilityId + "_rounded",
                HeroTexture = ownerName + "_rounded",
                MinimapHeroTexture = ownerName + "_icon",
                ShowUntil = Game.RawGameTime + this.TimeToShow,
                Position = position
            };

            owner.ChangeBasePosition(drawableAbility.Position);

            if (this.Replace)
            {
                var exist = drawableAbilities.LastOrDefault(
                    x => x.AbilityTexture == drawableAbility.AbilityTexture && x.HeroTexture == drawableAbility.HeroTexture);

                if (exist != null)
                {
                    drawableAbilities.Remove(exist);
                }
            }

            drawableAbilities.Add(drawableAbility);
        }
    }
}