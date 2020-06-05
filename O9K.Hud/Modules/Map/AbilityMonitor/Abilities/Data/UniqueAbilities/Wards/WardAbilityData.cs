namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Wards
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Helpers.Notificator;

    using SharpDX;

    internal class WardAbilityData : AbilityFullData
    {
        public string UnitName { get; set; }

        public void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, Vector3 position)
        {
            var drawableAbility = new DrawableWardAbility
            {
                AbilityUnitName = this.UnitName,
                AbilityTexture = this.AbilityId + "_rounded",
                Position = position,
                Duration = this.Duration,
                IsShowingRange = this.ShowRange,
                Range = this.Range,
                RangeColor = this.RangeColor,
                ShowUntil = Game.RawGameTime + this.Duration,
            };

            drawableAbility.DrawRange();
            drawableAbilities.Add(drawableAbility);
        }

        public override void AddDrawableAbility(List<IDrawableAbility> drawableAbilities, Unit unit, INotificator notificator)
        {
            var ward = drawableAbilities.OfType<DrawableWardAbility>()
                .FirstOrDefault(x => x.Unit == null && x.AbilityUnitName == this.UnitName && x.Position.Distance2D(unit.Position) < 400);
            if (ward != null)
            {
                ward.AddUnit(unit);
                return;
            }

            var mod = unit.Modifiers.FirstOrDefault(x => x.Name == "modifier_item_buff_ward");

            var drawableAbility = new DrawableWardAbility
            {
                AbilityUnitName = this.UnitName,
                AbilityTexture = this.AbilityId + "_rounded",
                Position = unit.Position,
                Unit = unit,
                Duration = this.Duration,
                IsShowingRange = this.ShowRange,
                Range = this.Range,
                RangeColor = this.RangeColor,
                AddedTime = Game.RawGameTime - (mod?.ElapsedTime ?? 0),
                ShowUntil = Game.RawGameTime + (mod?.RemainingTime ?? this.Duration)
            };

            drawableAbility.DrawRange();
            drawableAbilities.Add(drawableAbility);
        }
    }
}