namespace O9K.Evader.Abilities.Items.BlackKingBar
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Managers.Menu.Items;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class BlackKingBarUsable : CounterAbility
    {
        private readonly MenuToggleKey enabled;

        public BlackKingBarUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            this.enabled = menu.Hotkeys.BkbEnabled;
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!this.enabled.IsActive)
            {
                return false;
            }

            var ability = obstacle.EvadableAbility.Ability;

            if (ability.IsDisable())
            {
                if (ally.StatusResist >= 0.75)
                {
                    return false;
                }
            }
            else if (!ability.IsUltimate && obstacle.GetDamage(ally) < ally.Health)
            {
                return false;
            }

            return base.CanBeCasted(ally, enemy, obstacle);
        }
    }
}