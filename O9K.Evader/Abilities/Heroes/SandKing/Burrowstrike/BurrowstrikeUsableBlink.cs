namespace O9K.Evader.Abilities.Heroes.SandKing.Burrowstrike
{
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class BurrowstrikeUsableBlink : BlinkAbility
    {
        public BurrowstrikeUsableBlink(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return base.GetRequiredTime(ally, enemy, obstacle) + 0.15f;
        }
    }
}