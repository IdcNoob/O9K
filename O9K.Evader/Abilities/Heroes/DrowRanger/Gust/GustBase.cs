namespace O9K.Evader.Abilities.Heroes.DrowRanger.Gust
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.drow_ranger_wave_of_silence)]
    internal class GustBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public GustBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GustEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}