namespace O9K.Evader.Abilities.Base.Usable.CounterAbility
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class CounterHealAbility : CounterAbility
    {
        private readonly IHealthRestore healAbility;

        public CounterHealAbility(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            this.healAbility = ability as IHealthRestore;
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            var health = ally.Health;
            var damage = obstacle.GetDamage(ally);
            if (damage < health)
            {
                return false;
            }

            if (this.healAbility == null)
            {
                //grave etc.
                return true;
            }

            if (!ally.CanBeHealed)
            {
                return false;
            }

            if (ally.MaximumHealth - health < damage)
            {
                return false;
            }

            if (damage >= health + this.healAbility.GetHealthRestore(ally))
            {
                return false;
            }

            return true;
        }
    }
}