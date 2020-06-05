namespace O9K.Evader.Abilities.Heroes.PhantomLancer.Doppelganger
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class DoppelgangerUsable : CounterAbility
    {
        public DoppelgangerUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(ally.Position);
            return this.ActiveAbility.UseAbility(ally.InFront(100), false, true);
        }
    }
}