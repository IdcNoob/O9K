namespace O9K.Evader.Abilities.Heroes.KeeperOfTheLight.BlindingLight
{
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;

    using Metadata;

    using Pathfinder.Obstacles;

    using SharpDX;

    internal class BlindingLightUsable : DisableAbility
    {
        private Vector3 castPosition;

        public BlindingLightUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.castPosition = enemy.Position.Extend2D(ally.Position, 250);
            return this.ActiveAbility.GetHitTime(this.castPosition);
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(this.castPosition);
            return this.ActiveAbility.UseAbility(this.castPosition, false, true);
        }
    }
}