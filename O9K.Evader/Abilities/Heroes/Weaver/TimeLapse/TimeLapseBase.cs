namespace O9K.Evader.Abilities.Heroes.Weaver.TimeLapse
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.weaver_time_lapse)]
    internal class TimeLapseBase : EvaderBaseAbility, IEvadable
    {
        public TimeLapseBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TimeLapseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}