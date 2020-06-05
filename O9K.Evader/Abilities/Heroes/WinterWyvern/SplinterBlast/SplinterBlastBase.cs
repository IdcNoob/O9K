namespace O9K.Evader.Abilities.Heroes.WinterWyvern.SplinterBlast
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.winter_wyvern_splinter_blast)]
    internal class SplinterBlastBase : EvaderBaseAbility //, IEvadable
    {
        public SplinterBlastBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SplinterBlastEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}