namespace O9K.Evader.Abilities.Heroes.Tusk.WalrusPunch
{
    using Base;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tusk_walrus_punch)]
    internal class WalrusPunchBase : EvaderBaseAbility, IUsable<DisableAbility>
    {
        public WalrusPunchBase(Ability9 ability)
            : base(ability)
        {
            //todo evadable ?
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}