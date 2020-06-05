namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Wisp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Base;

    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Helpers.Notificator;

    internal class WispUnitData : AbilityFullData
    {
        public override void AddDrawableAbility(
            List<IDrawableAbility> drawableAbilities,
            ParticleEffect particle,
            Team allyTeam,
            INotificator notificator)
        {
            UpdateManager.BeginInvoke(
                () =>
                    {
                        try
                        {
                            if (!particle.IsValid)
                            {
                                return;
                            }

                            var owner = EntityManager9.GetUnit(particle.Owner.Handle);
                            if (owner == null)
                            {
                                return;
                            }

                            var drawableAbility = new DrawableWispUnit(owner, particle);

                            if (this.Replace)
                            {
                                var exist = drawableAbilities.LastOrDefault(x => x is DrawableWispUnit);

                                if (exist != null)
                                {
                                    drawableAbilities.Remove(exist);
                                }
                            }

                            UpdateManager.BeginInvoke(
                                async () =>
                                    {
                                        while (particle.IsValid)
                                        {
                                            owner.ChangeBasePosition(particle.Position);
                                            await Task.Delay(300);
                                        }
                                    });

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