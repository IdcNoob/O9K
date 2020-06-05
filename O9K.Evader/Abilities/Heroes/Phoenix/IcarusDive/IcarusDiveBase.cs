namespace O9K.Evader.Abilities.Heroes.Phoenix.IcarusDive
{
    using Base;
    using Base.Evadable;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.phoenix_icarus_dive)]
    internal class IcarusDiveBase : EvaderBaseAbility, IEvadable //, IUsable<BlinkAbility>
    {
        public IcarusDiveBase(Ability9 ability)
            : base(ability)
        {
            //todo usable ?
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new IcarusDiveEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkAbility(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}