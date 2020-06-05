namespace O9K.Evader.Abilities.Heroes.Bloodseeker.Rupture
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bloodseeker_rupture)]
    internal class RuptureBase : EvaderBaseAbility, IEvadable
    {
        public RuptureBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RuptureEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}