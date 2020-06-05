namespace O9K.Evader.Abilities.Heroes.Brewmaster.DispelMagic
{
    using Base;
    using Base.Usable.CounterAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.brewmaster_storm_dispel_magic)]
    internal class DispelMagicBase : EvaderBaseAbility, IUsable<CounterAbility>, IUsable<DisableAbility>
    {
        public DispelMagicBase(Ability9 ability)
            : base(ability)
        {
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