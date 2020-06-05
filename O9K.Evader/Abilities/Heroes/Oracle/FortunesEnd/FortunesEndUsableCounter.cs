namespace O9K.Evader.Abilities.Heroes.Oracle.FortunesEnd
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class FortunesEndUsableCounter : CounterAbility
    {
        private readonly IActionManager actionManager;

        public FortunesEndUsableCounter(Ability9 ability, IActionManager actionManager, IMainMenu menu)
            : base(ability, menu)
        {
            this.actionManager = actionManager;

            this.CanBeCastedOnAlly = true;
            this.CanBeCastedOnSelf = true;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(ally.Position);
            var use = this.ActiveAbility.UseAbility(ally, false, true);
            if (!use)
            {
                return false;
            }

            if (obstacle.IsModifierObstacle || obstacle.EvadableAbility.Ability.Id != AbilityId.silencer_global_silence)
            {
                this.actionManager.CancelChanneling(this.ActiveAbility);
            }

            return true;
        }
    }
}