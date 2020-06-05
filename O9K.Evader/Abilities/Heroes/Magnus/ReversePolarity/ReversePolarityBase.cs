namespace O9K.Evader.Abilities.Heroes.Magnus.ReversePolarity
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.magnataur_reverse_polarity)]
    internal class ReversePolarityBase : EvaderBaseAbility, IEvadable
    {
        public ReversePolarityBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ReversePolarityEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}