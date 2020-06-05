namespace O9K.Evader.Abilities.Items.HealingSalve
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_flask)]
    internal class HealingSalveBase : EvaderBaseAbility, IEvadable
    {
        public HealingSalveBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new HealingSalveEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}