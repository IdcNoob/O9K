namespace O9K.Evader.Abilities.Heroes.Alchemist.UnstableConcoction
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.alchemist_unstable_concoction)]
    internal class UnstableConcoctionBase : EvaderBaseAbility, IAllyEvadable
    {
        public UnstableConcoctionBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new UnstableConcoctionAllyEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}