namespace O9K.Evader.Abilities.Heroes.DarkWillow.Bedlam
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dark_willow_bedlam)]
    internal class BedlamBase : EvaderBaseAbility, IEvadable
    {
        public BedlamBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BedlamEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}