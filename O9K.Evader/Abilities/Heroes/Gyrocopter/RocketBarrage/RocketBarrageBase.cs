namespace O9K.Evader.Abilities.Heroes.Gyrocopter.RocketBarrage
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.gyrocopter_rocket_barrage)]
    internal class RocketBarrageBase : EvaderBaseAbility, IEvadable
    {
        public RocketBarrageBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RocketBarrageEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}