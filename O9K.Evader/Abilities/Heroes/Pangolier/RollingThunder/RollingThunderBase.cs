namespace O9K.Evader.Abilities.Heroes.Pangolier.RollingThunder
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.pangolier_gyroshell)]
    internal class RollingThunderBase : EvaderBaseAbility, IEvadable
    {
        public RollingThunderBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RollingThunderEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}