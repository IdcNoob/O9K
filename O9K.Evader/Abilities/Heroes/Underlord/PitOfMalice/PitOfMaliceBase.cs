namespace O9K.Evader.Abilities.Heroes.Underlord.PitOfMalice
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.abyssal_underlord_pit_of_malice)]
    internal class PitOfMaliceBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public PitOfMaliceBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PitOfMaliceEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}