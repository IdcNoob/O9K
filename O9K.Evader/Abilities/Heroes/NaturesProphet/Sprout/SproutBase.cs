namespace O9K.Evader.Abilities.Heroes.NaturesProphet.Sprout
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.furion_sprout)]
    internal class SproutBase : EvaderBaseAbility, IEvadable
    {
        public SproutBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SproutEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}