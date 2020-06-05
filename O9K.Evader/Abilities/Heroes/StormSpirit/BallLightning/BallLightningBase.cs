namespace O9K.Evader.Abilities.Heroes.StormSpirit.BallLightning
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.storm_spirit_ball_lightning)]
    internal class BallLightningBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public BallLightningBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new BallLightningUsable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}