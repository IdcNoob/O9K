namespace O9K.Evader.Abilities.Heroes.EmberSpirit.ActivateFireRemnant
{
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class ActivateFireRemnantUsable : BlinkAbility
    {
        public ActivateFireRemnantUsable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.GetCastDelay();
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var position = this.Owner.InFront(500);
            this.MoveCamera(position);
            return this.ActiveAbility.UseAbility(position, false, true);
        }
    }
}