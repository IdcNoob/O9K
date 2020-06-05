namespace O9K.Core.Entities.Units.Unique
{
    using Ensage;

    using Helpers;

    using Metadata;

    using SharpDX;

    [UnitName("npc_dota_brewmaster_fire_1")]
    [UnitName("npc_dota_brewmaster_fire_2")]
    [UnitName("npc_dota_brewmaster_fire_3")]
    [UnitName("npc_dota_brewmaster_storm_1")]
    [UnitName("npc_dota_brewmaster_storm_2")]
    [UnitName("npc_dota_brewmaster_storm_3")]
    [UnitName("npc_dota_brewmaster_earth_1")]
    [UnitName("npc_dota_brewmaster_earth_2")]
    [UnitName("npc_dota_brewmaster_earth_3")]
    internal class BrewmasterPanda : Unit9
    {
        private Vector2 healthBarPositionCorrection;

        private Vector2 healthBarSize;

        public BrewmasterPanda(Unit baseUnit)
            : base(baseUnit)
        {
        }

        public override Vector2 HealthBarSize
        {
            get
            {
                if (this.healthBarSize.IsZero)
                {
                    this.healthBarSize = new Vector2(Hud.Info.ScreenRatio * 100, Hud.Info.ScreenRatio * 8);
                }

                return this.healthBarSize;
            }
        }

        protected override Vector2 HealthBarPositionCorrection
        {
            get
            {
                if (this.healthBarPositionCorrection.IsZero)
                {
                    this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 2f, Hud.Info.ScreenRatio * 23);
                }

                return this.healthBarPositionCorrection;
            }
        }
    }
}