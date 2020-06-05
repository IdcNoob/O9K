namespace O9K.Evader.Abilities.Items.Clarity
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_clarity)]
    internal class ClarityBase : EvaderBaseAbility, IEvadable
    {
        public ClarityBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ClarityEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}