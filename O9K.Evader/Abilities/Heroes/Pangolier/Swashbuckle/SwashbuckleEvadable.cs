namespace O9K.Evader.Abilities.Heroes.Pangolier.Swashbuckle
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class SwashbuckleEvadable : LinearAreaOfEffectEvadable
    {
        public SwashbuckleEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo fix

            this.Counters.Add(Abilities.Bulwark);
        }
    }
}