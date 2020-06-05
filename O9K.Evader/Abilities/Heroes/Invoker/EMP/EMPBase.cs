namespace O9K.Evader.Abilities.Heroes.Invoker.EMP
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.invoker_emp)]
    // ReSharper disable once InconsistentNaming
    internal class EMPBase : EvaderBaseAbility, IEvadable
    {
        public EMPBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EMPEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}