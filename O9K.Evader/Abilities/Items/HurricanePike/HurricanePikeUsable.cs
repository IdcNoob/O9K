namespace O9K.Evader.Abilities.Items.HurricanePike
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class HurricanePikeUsable : CounterEnemyAbility
    {
        public HurricanePikeUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            //todo cast on allies ?
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return base.GetRequiredTime(ally, enemy, obstacle) + 0.1f;
        }
    }
}