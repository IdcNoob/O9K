namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Cleave
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;

    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using Helpers.Notificator;

    internal class CleaveAbilityData : AbilityFullData
    {
        public override void AddDrawableAbility(
            List<IDrawableAbility> drawableAbilities,
            ParticleEffect particle,
            Team allyTeam,
            INotificator notificator)
        {
            var owner = EntityManager9.GetUnit(particle.Owner.Handle);

            if (owner?.IsVisible != false)
            {
                return;
            }

            var position = this.RawParticlePosition ? particle.Position : particle.GetControlPoint(this.ControlPoint);
            if (position.IsZero || position.Distance2D(owner.BaseUnit.Position) < 50)
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
        }
    }
}