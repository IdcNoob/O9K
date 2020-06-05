namespace O9K.Evader.Abilities.Heroes.AncientApparition.ColdFeet
{
    using Base;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Modifiers;

    internal class ColdFeetEvadableModifier : ModifierAllyObstacle
    {
        private readonly float duration;

        public ColdFeetEvadableModifier(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner, float duration)
            : base(ability, modifier, modifierOwner)
        {
            this.duration = duration;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            if (!this.Modifier.IsValid)
            {
                return 0;
            }

            return this.duration - this.Modifier.ElapsedTime;
        }
    }
}