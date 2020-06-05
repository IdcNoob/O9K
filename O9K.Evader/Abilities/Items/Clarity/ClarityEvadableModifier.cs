namespace O9K.Evader.Abilities.Items.Clarity
{
    using Base;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Modifiers;

    internal class ClarityEvadableModifier : ModifierEnemyObstacle
    {
        public ClarityEvadableModifier(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner, float range)
            : base(ability, modifier, modifierOwner, range)
        {
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (!base.IsIntersecting(unit, checkPrediction))
            {
                return false;
            }

            if (this.ModifierOwner.ManaPercentage > 90 || this.Modifier.RemainingTime < 15)
            {
                return false;
            }

            return true;
        }
    }
}