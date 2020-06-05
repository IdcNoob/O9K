namespace O9K.Evader.Pathfinder.Obstacles.Modifiers
{
    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base;

    internal class ModifierStackAllyObstacle : ModifierAllyObstacle
    {
        private readonly int stacks;

        public ModifierStackAllyObstacle(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner, int stacks)
            : base(ability, modifier, modifierOwner)
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

            return this.ModifierOwner.Equals(unit) && this.Modifier.StackCount >= this.stacks;
        }
    }
}