namespace O9K.Evader.Abilities.Heroes.Puck.PhaseShift
{
    using System;

    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class PhaseShiftUsable : CounterAbility
    {
        private readonly IActionManager actionManager;

        private readonly IChanneled channeled;

        public PhaseShiftUsable(Ability9 ability, IActionManager actionManager, IMainMenu menu)
            : base(ability, menu)
        {
            this.channeled = (IChanneled)ability;
            this.actionManager = actionManager;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.Use(ally, enemy, obstacle))
            {
                return false;
            }

            this.Owner.SetExpectedUnitState(UnitState.Invulnerable);
            this.actionManager.BlockInput(this.Owner, Math.Min(this.channeled.ChannelTime, 1));

            return true;
        }
    }
}