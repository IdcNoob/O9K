namespace O9K.Evader.Abilities.Items.EtherealBlade
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class EtherealBladeUsableCounter : CounterAbility
    {
        public EtherealBladeUsableCounter(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.GetCastDelay(ally);
        }
    }
}