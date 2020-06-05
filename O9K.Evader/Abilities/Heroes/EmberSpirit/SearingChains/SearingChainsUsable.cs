namespace O9K.Evader.Abilities.Heroes.EmberSpirit.SearingChains
{
    using System.Linq;

    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class SearingChainsUsable : DisableAbility
    {
        public SearingChainsUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            if (EntityManager9.Units.Count(x => x.IsUnit && x.IsAlive && !x.IsMagicImmune && !x.IsInvulnerable && x.IsEnemy(this.Owner))
                > 3)
            {
                return false;
            }

            return true;
        }
    }
}