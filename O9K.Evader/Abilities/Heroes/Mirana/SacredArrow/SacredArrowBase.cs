namespace O9K.Evader.Abilities.Heroes.Mirana.SacredArrow
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.mirana_arrow)]
    internal class SacredArrowBase : EvaderBaseAbility, IEvadable
    {
        public SacredArrowBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SacredArrowEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}