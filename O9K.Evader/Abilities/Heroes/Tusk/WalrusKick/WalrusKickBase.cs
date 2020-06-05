namespace O9K.Evader.Abilities.Heroes.Tusk.WalrusKick
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tusk_walrus_kick)]
    internal class WalrusKickBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public WalrusKickBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WalrusKickEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}