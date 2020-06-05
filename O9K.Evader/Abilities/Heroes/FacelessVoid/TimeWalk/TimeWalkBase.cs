namespace O9K.Evader.Abilities.Heroes.FacelessVoid.TimeWalk
{
    using Base;
    using Base.Evadable;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.faceless_void_time_walk)]
    internal class TimeWalkBase : EvaderBaseAbility, IEvadable, IUsable<BlinkAbility>
    {
        public TimeWalkBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TimeWalkEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkAbility(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}