namespace O9K.Evader.Abilities.Heroes.Pangolier.Swashbuckle
{
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class SwashbuckleUsable : BlinkAbility
    {
        public SwashbuckleUsable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return base.GetRequiredTime(ally, enemy, obstacle) + 0.1f;
        }
    }
}