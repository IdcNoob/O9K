namespace O9K.Evader.Abilities.Items.Satanic
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class SatanicUsable : CounterAbility
    {
        public SatanicUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            if (!ally.CanAttack())
            {
                return false;
            }

            return true;
        }
    }
}