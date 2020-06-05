namespace O9K.Evader.Abilities.Heroes.DarkWillow.ShadowRealm
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class ShadowRealmUsable : CounterAbility
    {
        public ShadowRealmUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(this.Owner.Position);
            return this.ActiveAbility.UseAbility(false, true);
        }
    }
}