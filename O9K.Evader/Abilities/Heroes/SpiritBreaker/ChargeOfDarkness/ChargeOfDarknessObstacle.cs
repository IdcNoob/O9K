namespace O9K.Evader.Abilities.Heroes.SpiritBreaker.ChargeOfDarkness
{
    using Base.Evadable;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Abilities;

    internal class ChargeOfDarknessObstacle : AbilityObstacle
    {
        private readonly Modifier modifier;

        private readonly Unit modifierOwner;

        public ChargeOfDarknessObstacle(EvadableAbility ability, Modifier modifier, Unit ally)
            : base(ability)
        {
            this.modifier = modifier;
            this.modifierOwner = ally;
        }

        public override bool IsExpired
        {
            get
            {
                return !this.modifier.IsValid;
            }
        }

        public override void Draw()
        {
            this.Drawer.DrawCircle(this.modifierOwner.Position, 100);
            this.Drawer.UpdateCirclePosition(this.modifierOwner.Position);
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return (this.Caster.Distance(this.modifierOwner.Position) - this.EvadableAbility.ActiveAbility.Radius)
                   / this.EvadableAbility.ActiveAbility.Speed;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            return (this.Caster.Distance(this.modifierOwner.Position) - (this.EvadableAbility.ActiveAbility.Radius - 100))
                   / this.EvadableAbility.ActiveAbility.Speed;
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (!this.Caster.IsVisible)
            {
                return false;
            }

            return unit.Handle == this.modifierOwner.Handle;
        }
    }
}