namespace O9K.Evader.Abilities.Items.DiffusalBlade
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_diffusal_blade)]
    internal class DiffusalBladeBase : EvaderBaseAbility, IEvadable
    {
        public DiffusalBladeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DiffusalBladeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}