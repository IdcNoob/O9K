namespace O9K.Evader.Abilities.Heroes.Techies.ProximityMines
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.techies_land_mines)]
    internal class ProximityMinesBase : EvaderBaseAbility //, IEvadable
    {
        public ProximityMinesBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ProximityMinesEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}