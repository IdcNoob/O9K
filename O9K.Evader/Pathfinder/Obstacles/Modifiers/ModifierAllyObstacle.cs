namespace O9K.Evader.Pathfinder.Obstacles.Modifiers
{
    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base;

    internal class ModifierAllyObstacle : ModifierObstacle
    {
        public ModifierAllyObstacle(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner)
            : base(ability, modifier, modifierOwner)
        {
            this.Caster = this.EvadableAbility.Owner;
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            return this.ModifierOwner.Equals(unit);
        }
    }
}