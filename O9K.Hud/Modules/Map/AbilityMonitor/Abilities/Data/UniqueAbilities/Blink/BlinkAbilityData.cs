namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Blink
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base;

    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Helpers.Notificator;

    internal class BlinkAbilityData : AbilityFullData
    {
        public override void AddDrawableAbility(
            List<IDrawableAbility> drawableAbilities,
            ParticleEffect particle,
            Team allyTeam,
            INotificator notificator)
        {
            var particleOwner = particle.Owner;
            var particlePosition = particle.GetControlPoint(this.ControlPoint);

            UpdateManager.BeginInvoke(
                () =>
                    {
                        try
                        {
                            var owner = this.SearchOwner || !(particleOwner is Unit)
                                            ? EntityManager9.Abilities
                                                .FirstOrDefault(x => x.Id == this.AbilityId && x.Owner.Team != allyTeam)
                                                ?.Owner
                                            : EntityManager9.GetUnit(particleOwner.Handle);

                            if (owner?.IsVisible != false)
                            {
                                return;
                            }

                            if (Game.RawGameTime - owner.LastVisibleTime < 1)
                            {
                                var ability = owner.Abilities.FirstOrDefault(x => x.Id == this.AbilityId);
                                if (ability == null)
                                {
                                    return;
                                }

                                var drawableAbility = new DrawableAbility
                                {
                                    AbilityTexture = this.AbilityId + "_rounded",
                                    HeroTexture = owner.Name + "_rounded",
                                    MinimapHeroTexture = owner.Name + "_icon",
                                    ShowUntil = Game.RawGameTime + this.TimeToShow,
                                    Position = owner.InFront(ability.Range)
                                };

                                owner.ChangeBasePosition(drawableAbility.Position);
                                drawableAbilities.Add(drawableAbility);
                            }
                            else
                            {
                                var drawableAbility = new DrawableAbility
                                {
                                    AbilityTexture = this.AbilityId + "_rounded",
                                    HeroTexture = owner.Name + "_rounded",
                                    MinimapHeroTexture = owner.Name + "_icon",
                                    ShowUntil = Game.RawGameTime + this.TimeToShow,
                                    Position = particlePosition
                                };

                                owner.ChangeBasePosition(drawableAbility.Position);
                                drawableAbilities.Add(drawableAbility);
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });
        }
    }
}