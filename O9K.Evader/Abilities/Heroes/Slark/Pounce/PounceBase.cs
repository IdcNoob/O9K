namespace O9K.Evader.Abilities.Heroes.Slark.Pounce
{
    using Base;
    using Base.Evadable;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.slark_pounce)]
    internal class PounceBase : EvaderBaseAbility, IEvadable, IUsable<BlinkAbility>
    {
        public PounceBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PounceEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkLeapAbility(this.Ability, this.Pathfinder, this.ActionManager, this.Menu);
        }
    }
}