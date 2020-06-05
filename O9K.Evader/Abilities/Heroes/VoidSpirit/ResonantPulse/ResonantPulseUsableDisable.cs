namespace O9K.Evader.Abilities.Heroes.VoidSpirit.ResonantPulse
{
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class ResonantPulseUsableDisable : DisableAbility
    {
        public ResonantPulseUsableDisable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!this.Owner.HasAghanimsScepter)
            {
                return false;
            }

            return base.CanBeCasted(ally, enemy, obstacle);
        }
    }
}