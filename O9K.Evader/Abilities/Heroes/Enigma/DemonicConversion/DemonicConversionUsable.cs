namespace O9K.Evader.Abilities.Heroes.Enigma.DemonicConversion
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class DemonicConversionUsable : CounterAbility
    {
        public DemonicConversionUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return base.Use(enemy, enemy, obstacle);
        }
    }
}