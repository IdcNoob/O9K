namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.ThinkerAbility
{
    using System.Collections.Generic;

    using Base;

    using Core.Managers.Entity;

    using Ensage;

    using Helpers.Notificator;

    internal class ThinkerUnitAbilityData : AbilityFullData
    {
        public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, Unit unit, INotificator notificator)
        {
            if (!(unit.Owner is Hero owner))
            {
                return;
            }

            switch (owner.HeroId)
            {
                case HeroId.npc_dota_hero_tinker:
                {
                    this.AbilityId = AbilityId.tinker_march_of_the_machines;
                    break;
                }
                default:
                {
                    return;
                }
            }

            var ownerName = owner.Name;
            var drawableAbility = new DrawableUnitAbility
            {
                AbilityTexture = this.AbilityId + "_rounded",
                HeroTexture = ownerName + "_rounded",
                MinimapHeroTexture = ownerName + "_icon",
                Position = unit.Position,
                Unit = unit,
                Duration = this.Duration,
                ShowUntil = Game.RawGameTime + this.Duration,
                ShowHeroUntil = Game.RawGameTime + this.TimeToShow,
                ShowTimer = this.ShowTimer,
                Owner = owner,
            };

            EntityManager9.GetUnit(owner.Handle)?.ChangeBasePosition(drawableAbility.Position);

            drawableAbilities.Add(drawableAbility);
        }
    }
}