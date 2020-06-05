namespace O9K.Evader.Abilities.Items.Mjollnir
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_mjollnir)]
    internal class MjollnirBase : EvaderBaseAbility, IEvadable
    {
        public MjollnirBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MjollnirEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}