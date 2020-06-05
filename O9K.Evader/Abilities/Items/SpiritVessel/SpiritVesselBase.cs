namespace O9K.Evader.Abilities.Items.SpiritVessel
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_spirit_vessel)]
    internal class SpiritVesselBase : EvaderBaseAbility, IEvadable
    {
        public SpiritVesselBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SpiritVesselEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}