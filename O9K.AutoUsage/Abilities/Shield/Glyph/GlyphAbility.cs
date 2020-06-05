namespace O9K.AutoUsage.Abilities.Shield.Glyph
{
    using System;
    using System.Linq;

    using Core.Data;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;

    internal class GlyphAbility
    {
        private readonly Sleeper sleeper = new Sleeper();

        private Team team;

        private float GlyphCooldown
        {
            get
            {
                return this.team == Team.Radiant ? Game.GlyphCooldownRadiant : Game.GlyphCooldownDire;
            }
        }

        public void Activate()
        {
            this.team = EntityManager9.Owner.Team;
            EntityManager9.UnitMonitor.UnitHealthChange += this.OnUnitHealthChange;
        }

        public void Deactivate()
        {
            EntityManager9.UnitMonitor.UnitHealthChange += this.OnUnitHealthChange;
        }

        private void OnUnitHealthChange(Unit9 unit, float health)
        {
            try
            {
                if (!unit.IsTower || unit.Team != this.team || health > 100 || !unit.Name.Contains("tower1"))
                {
                    return;
                }

                if (this.GlyphCooldown > 0 || this.sleeper)
                {
                    return;
                }

                if (!EntityManager9.Units.Any(x => x.IsUnit && x.Team != this.team && x.IsVisible && x.IsAlive && x.Distance(unit) < 1000))
                {
                    return;
                }

                Player.Glyph();
                this.sleeper.Sleep(GameData.GlyphDuration);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}