namespace O9K.Evader.Abilities.Heroes.Chen.HolyPersuasion
{
    using Base;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Modifiers;

    internal class HolyPersuasionEvadableModifier : ModifierEnemyObstacle
    {
        public HolyPersuasionEvadableModifier(IModifierCounter ability, Modifier modifier, Unit9 modifierOwner, float range)
            : base(ability, modifier, modifierOwner, range)
        {
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return this.Modifier.RemainingTime - 0.2f - this.Delay;
        }
    }
}