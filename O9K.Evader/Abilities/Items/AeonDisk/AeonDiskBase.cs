namespace O9K.Evader.Abilities.Items.AeonDisk
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_aeon_disk)]
    internal class AeonDiskBase : EvaderBaseAbility, IEvadable
    {
        public AeonDiskBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new AeonDiskEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}