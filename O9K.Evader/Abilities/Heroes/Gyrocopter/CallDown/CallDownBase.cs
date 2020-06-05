namespace O9K.Evader.Abilities.Heroes.Gyrocopter.CallDown
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.gyrocopter_call_down)]
    internal class CallDownBase : EvaderBaseAbility, IEvadable
    {
        public CallDownBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new CallDownEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}