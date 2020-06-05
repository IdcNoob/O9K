namespace O9K.Evader.Abilities.Units.NecronomiconArcher.Purge
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.necronomicon_archer_purge)]
    internal class PurgeBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public PurgeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PurgeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}