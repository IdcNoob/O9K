namespace O9K.Evader.Abilities.Heroes.Enchantress.Spoink
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class SpoinkUsable : CounterAbility
    {
        public SpoinkUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.GetHitTime(ally) + 0.1f;
        }
    }
}