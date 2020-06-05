namespace O9K.Evader.Abilities.Heroes.Tinker.Rearm
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tinker_rearm)]
    internal class RearmBase : EvaderBaseAbility //, IEvadable
    {
        public RearmBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RearmEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}