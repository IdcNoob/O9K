namespace O9K.Evader.Abilities.Heroes.NyxAssassin.Vendetta
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.nyx_assassin_vendetta)]
    internal class VendettaBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public VendettaBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterInvisibilityAbility(this.Ability, this.Menu);
        }
    }
}