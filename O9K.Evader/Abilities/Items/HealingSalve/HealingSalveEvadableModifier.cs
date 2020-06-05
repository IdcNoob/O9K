namespace O9K.Evader.Abilities.Items.HealingSalve
{
    using Base;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Modifiers;

    internal class HealingSalveEvadableModifier : ModifierEnemyObstacle
    {
        public HealingSalveEvadableModifier(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner, float range)
            : base(ability, modifier, modifierOwner, range)
        {
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (!base.IsIntersecting(unit, checkPrediction))
            {
                return false;
            }

            if (this.ModifierOwner.HealthPercentage > 90 || this.Modifier.RemainingTime < 3)
            {
                return false;
            }

            return true;
        }
    }
}