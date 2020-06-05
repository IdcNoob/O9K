namespace O9K.Evader.Pathfinder.Obstacles.Modifiers
{
    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base;

    internal class ModifierEnemyObstacle : ModifierObstacle
    {
        public ModifierEnemyObstacle(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner, float range)
            : base(ability, modifier, modifierOwner)
        {
            this.Caster = modifierOwner;
            this.Range = range;
            this.CreateTime = Game.RawGameTime;
        }

        protected float CreateTime { get; set; }

        protected float Range { get; }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (!this.Modifier.IsValid)
            {
                return false;
            }

            if (Game.RawGameTime < this.CreateTime + 0.3f)
            {
                return false;
            }

            if (this.Range <= 0)
            {
                return true;
            }

            return this.ModifierOwner.Distance(unit) <= this.Range;
        }
    }
}