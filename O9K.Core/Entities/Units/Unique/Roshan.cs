namespace O9K.Core.Entities.Units.Unique
{
    using Ensage;

    using Helpers;

    using Metadata;

    using SharpDX;

    [UnitName("npc_dota_roshan")]
    public class Roshan : Unit9
    {
        private Vector2 healthBarPositionCorrection;

        private Vector2 healthBarSize;

        public Roshan(Unit baseUnit)
            : base(baseUnit)
        {
        }

        public override Vector2 HealthBarSize
        {
            get
            {
                if (this.healthBarSize.IsZero)
                {
                    this.healthBarSize = new Vector2(Hud.Info.ScreenRatio * 218, Hud.Info.ScreenRatio * 5);
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
                    this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 2f, Hud.Info.ScreenRatio * 9);
                }

                return this.healthBarPositionCorrection;
            }
        }
    }
}