namespace O9K.Evader.Abilities.Heroes.FacelessVoid.Chronosphere
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.faceless_void_chronosphere)]
    internal class ChronosphereBase : EvaderBaseAbility, IEvadable, IAllyEvadable
    {
        public ChronosphereBase(Ability9 ability)
            : base(ability)
        {
        }

        EvadableAbility IAllyEvadable.GetEvadableAbility()
        {
            return new ChronosphereAllyEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        EvadableAbility IEvadable.GetEvadableAbility()
        {
            return new ChronosphereEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}