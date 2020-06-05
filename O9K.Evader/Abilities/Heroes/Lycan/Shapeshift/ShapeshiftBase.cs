namespace O9K.Evader.Abilities.Heroes.Lycan.Shapeshift
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lycan_shapeshift)]
    internal class ShapeshiftBase : EvaderBaseAbility, IEvadable
    {
        public ShapeshiftBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShapeshiftEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}