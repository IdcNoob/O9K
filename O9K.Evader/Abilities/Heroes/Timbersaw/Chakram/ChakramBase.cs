namespace O9K.Evader.Abilities.Heroes.Timbersaw.Chakram
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.shredder_chakram)]
    [AbilityId(AbilityId.shredder_chakram_2)]
    internal class ChakramBase : EvaderBaseAbility //, IEvadable
    {
        public ChakramBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ChakramEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}