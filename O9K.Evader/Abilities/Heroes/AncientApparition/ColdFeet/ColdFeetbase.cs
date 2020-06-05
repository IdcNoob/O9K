namespace O9K.Evader.Abilities.Heroes.AncientApparition.ColdFeet
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ancient_apparition_cold_feet)]
    internal class ColdFeetBase : EvaderBaseAbility, IEvadable
    {
        public ColdFeetBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ColdFeetEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}