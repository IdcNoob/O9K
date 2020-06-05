namespace O9K.Evader.Abilities.Heroes.Axe.BerserkersCall
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.axe_berserkers_call)]
    internal class BerserkersCallBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public BerserkersCallBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BerserkersCallEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}