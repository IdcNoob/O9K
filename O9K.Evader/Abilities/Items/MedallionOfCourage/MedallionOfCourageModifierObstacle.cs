namespace O9K.Evader.Abilities.Items.MedallionOfCourage
{
    using System.Linq;

    using Base;

    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using Pathfinder.Obstacles.Modifiers;

    internal class MedallionOfCourageModifierObstacle : ModifierAllyObstacle
    {
        public MedallionOfCourageModifierObstacle(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner)
            : base(ability, modifier, modifierOwner)
        {
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (!EntityManager9.Heroes.Any(
                    x => x.IsVisible && x.IsAlive && x.IsEnemy(this.ModifierOwner) && !x.IsIllusion
                         && x.Distance(this.ModifierOwner) < 900))
            {
                return false;
            }

            return this.ModifierOwner.Equals(unit);
        }
    }
}