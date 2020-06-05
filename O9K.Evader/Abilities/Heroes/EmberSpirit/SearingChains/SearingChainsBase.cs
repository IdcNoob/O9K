namespace O9K.Evader.Abilities.Heroes.EmberSpirit.SearingChains
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ember_spirit_searing_chains)]
    internal class SearingChainsBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public SearingChainsBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SearingChainsEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new SearingChainsUsable(this.Ability, this.Menu);
        }
    }
}