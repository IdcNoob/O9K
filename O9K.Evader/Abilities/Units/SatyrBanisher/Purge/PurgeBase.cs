namespace O9K.Evader.Abilities.Units.SatyrBanisher.Purge
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.satyr_trickster_purge)]
    internal class PurgeBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>, IUsable<DisableAbility>
    {
        public PurgeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PurgeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        CounterAbility IUsable<CounterAbility>.GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }

        DisableAbility IUsable<DisableAbility>.GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}