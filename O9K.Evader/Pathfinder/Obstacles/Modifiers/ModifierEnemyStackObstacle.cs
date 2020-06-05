namespace O9K.Evader.Pathfinder.Obstacles.Modifiers
{
    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base;

    internal class ModifierEnemyStackObstacle : ModifierEnemyObstacle
    {
        private readonly int stacks;

        public ModifierEnemyStackObstacle(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner, float range, int stacks)
            : base(ability, modifier, modifierOwner, range)
        {
            this.stacks = stacks;
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (!this.Modifier.IsValid)
            {
                //todo remove this if updatemanager is used for onupdate
                return false;
            }

            if (this.Modifier.StackCount < this.stacks)
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