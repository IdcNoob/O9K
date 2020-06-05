namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Item
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;

    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Helpers.Notificator;
    using Helpers.Notificator.Notifications;

    internal class NoOwnerItemData : AbilityFullData
    {
        public float RangeCheck { get; set; }

        public override void AddDrawableAbility(
            List<IDrawableAbility> drawableAbilities,
            ParticleEffect particle,
            Team allyTeam,
            INotificator notificator)
        {
            var position = this.RawParticlePosition ? particle.Position : particle.GetControlPoint(this.ControlPoint);
            if (position.IsZero)
            {
                Logger.Error("ParticleZero", particle.Name);
                return;
            }

            var owners = EntityManager9.Abilities.Where(x => x.Id == this.AbilityId)
                .Select(x => x.Owner)
                .Where(x => x.CanUseAbilities && x.IsAlive)
                .ToList();

            if (owners.Any(x => x.IsVisible && x.Distance(position) <= this.RangeCheck))
            {
                return;
            }

            var owner = owners.Find(x => x.Team != allyTeam && !x.IsVisible);
            if (owner == null)
            {
                return;
            }

            var ownerName = owner.Name;

            var drawableAbility = new DrawableAbility
            {
                AbilityTexture = this.AbilityId + "_rounded",
                HeroTexture = ownerName + "_rounded",
                MinimapHeroTexture = ownerName + "_icon",
                ShowUntil = Game.RawGameTime + this.TimeToShow,
                Position = position.SetZ(350)
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

            if (this.ShowNotification)
            {
                notificator?.PushNotification(new AbilityNotification(ownerName, this.AbilityId.ToString()));
            }
        }
    }
}