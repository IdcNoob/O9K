namespace O9K.Evader.Abilities.Heroes.Snapfire.Scatterblast
{
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.Snapfire;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class ScatterblastUsable : DisableAbility
    {
        private readonly Scatterblast scatterblast;

        public ScatterblastUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            this.scatterblast = (Scatterblast)ability;
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            if (!this.scatterblast.AppliesDisarm)
            {
                return false;
            }

            return true;
        }
    }
}