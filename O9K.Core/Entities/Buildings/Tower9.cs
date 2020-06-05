namespace O9K.Core.Entities.Buildings
{
    using Ensage;

    using Helpers;

    using Managers.Entity;

    using SharpDX;

    using Units;

    public class Tower9 : Building9
    {
        private Vector2 healthBarPositionCorrection;

        private Vector2 healthBarSize;

        public Tower9(Tower baseUnit)
            : base(baseUnit)
        {
            this.BaseTower = baseUnit;
            this.IsTower = true;
        }

        public Tower BaseTower { get; }

        public override Vector2 HealthBarSize
        {
            get
            {
                if (this.healthBarSize.IsZero)
                {
                    this.healthBarSize = new Vector2(Hud.Info.ScreenRatio * 108, Hud.Info.ScreenRatio * 5);
                }

                return this.healthBarSize;
            }
        }

        public Unit9 TowerTarget
        {
            get
            {
                var target = this.BaseTower.AttackTarget;
                if (target?.IsValid == true)
                {
                    return EntityManager9.GetUnitFast(target.Handle);
                }

                return null;
            }
        }

        protected override Vector2 HealthBarPositionCorrection
        {
            get
            {
                if (this.healthBarPositionCorrection.IsZero)
                {
                    this.healthBarPositionCorrection = new Vector2(this.HealthBarSize.X / 2f, Hud.Info.ScreenRatio * 63);
                }

                return this.healthBarPositionCorrection;
            }
        }
    }
}