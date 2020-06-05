namespace O9K.Evader.Abilities.Heroes.VoidSpirit.Dissimilate
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class DissimilateUsable : CounterAbility
    {
        public DissimilateUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return base.GetRequiredTime(ally, enemy, obstacle) - this.ActiveAbility.ActivationDelay;
        }
    }
}