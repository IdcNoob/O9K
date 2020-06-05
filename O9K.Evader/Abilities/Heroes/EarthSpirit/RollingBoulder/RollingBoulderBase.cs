namespace O9K.Evader.Abilities.Heroes.EarthSpirit.RollingBoulder
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.earth_spirit_rolling_boulder)]
    internal class RollingBoulderBase : EvaderBaseAbility, IEvadable
    {
        public RollingBoulderBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RollingBoulderEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}