namespace O9K.Evader.Abilities.Heroes.QueenOfPain.ScreamOfPain
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.queenofpain_scream_of_pain)]
    internal class ScreamOfPainBase : EvaderBaseAbility, IEvadable
    {
        public ScreamOfPainBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ScreamOfPainEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}