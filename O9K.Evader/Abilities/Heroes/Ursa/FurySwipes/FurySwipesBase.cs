namespace O9K.Evader.Abilities.Heroes.Ursa.FurySwipes
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ursa_fury_swipes)]
    internal class FurySwipesBase : EvaderBaseAbility, IEvadable
    {
        public FurySwipesBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FurySwipesEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}