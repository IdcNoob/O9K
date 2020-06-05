namespace O9K.Core.Entities.Units.Unique
{
    using Ensage;

    using Helpers;

    using Metadata;

    using SharpDX;

    [UnitName("npc_dota_lone_druid_bear1")]
    [UnitName("npc_dota_lone_druid_bear2")]
    [UnitName("npc_dota_lone_druid_bear3")]
    [UnitName("npc_dota_lone_druid_bear4")]
    public class SpiritBear : Unit9
    {
        private Vector2 healthBarPositionCorrection;

        private Vector2 healthBarSize;

        public SpiritBear(Unit baseUnit)
            : base(baseUnit)
        {
            this.CanUseItems = true;
        }

        public override Vector2 HealthBarSize
        {
            get
            {
                if (this.healthBarSize.IsZero)
                {
                    if (this.IsAlly())
                    {
                        this.healthBarSize = new Vector2(Hud.Info.ScreenRatio * 100, Hud.Info.ScreenRatio * 6);
                    }
                    else
                    {
                        this.healthBarSize = new Vector2(Hud.Info.ScreenRatio * 100, Hud.Info.ScreenRatio * 8);
                    }
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