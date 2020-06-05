namespace O9K.Evader.Abilities.Heroes.DrowRanger.Multishot
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.drow_ranger_multishot)]
    internal class MultishotBase : EvaderBaseAbility, IEvadable
    {
        public MultishotBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MultishotEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}