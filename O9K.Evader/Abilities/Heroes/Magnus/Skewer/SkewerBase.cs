namespace O9K.Evader.Abilities.Heroes.Magnus.Skewer
{
    using Base;
    using Base.Evadable;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.magnataur_skewer)]
    internal class SkewerBase : EvaderBaseAbility, IEvadable, IUsable<BlinkAbility>
    {
        public SkewerBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SkewerEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkAbility(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}