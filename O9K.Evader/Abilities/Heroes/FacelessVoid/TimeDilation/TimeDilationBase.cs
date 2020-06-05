namespace O9K.Evader.Abilities.Heroes.FacelessVoid.TimeDilation
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.faceless_void_time_dilation)]
    internal class TimeDilationBase : EvaderBaseAbility, IEvadable
    {
        public TimeDilationBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TimeDilationEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}