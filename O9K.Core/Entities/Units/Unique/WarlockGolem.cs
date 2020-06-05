namespace O9K.Core.Entities.Units.Unique
{
    using Ensage;

    using Helpers;

    using Metadata;

    using SharpDX;

    [UnitName("npc_dota_warlock_golem_1")]
    [UnitName("npc_dota_warlock_golem_2")]
    [UnitName("npc_dota_warlock_golem_3")]
    [UnitName("npc_dota_warlock_golem_scepter_1")]
    [UnitName("npc_dota_warlock_golem_scepter_2")]
    [UnitName("npc_dota_warlock_golem_scepter_3")]
    internal class WarlockGolem : Unit9
    {
        private Vector2 healthBarPositionCorrection;

        private Vector2 healthBarSize;

        public WarlockGolem(Unit baseUnit)
            : base(baseUnit)
        {
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