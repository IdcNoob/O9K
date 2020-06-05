namespace O9K.Evader.Abilities.Heroes.Alchemist.UnstableConcoction
{
    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.Alchemist;
    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Modifiers;

    internal class UnstableConcoctionAllyObstacle : ModifierAllyObstacle
    {
        private readonly UnstableConcoction unstableConcoction;

        public UnstableConcoctionAllyObstacle(IModifierCounter modifierCounter, Ability9 ability, Modifier modifier, Unit9 modifierOwner)
            : base(modifierCounter, modifier, modifierOwner)
        {
            this.unstableConcoction = (UnstableConcoction)ability;
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return 0;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            if (!this.Modifier.IsValid)
            {
                return 0;
            }

            return this.unstableConcoction.BrewExplosion - this.Modifier.ElapsedTime - this.Delay;
        }
    }
}