namespace O9K.Evader.Abilities.Heroes.CentaurWarrunner.Stampede
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.centaur_stampede)]
    internal class StampedeBase : EvaderBaseAbility, IEvadable
    {
        public StampedeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new StampedeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}