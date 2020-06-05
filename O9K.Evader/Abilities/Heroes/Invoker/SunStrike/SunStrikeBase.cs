namespace O9K.Evader.Abilities.Heroes.Invoker.SunStrike
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.invoker_sun_strike)]
    internal class SunStrikeBase : EvaderBaseAbility, IEvadable
    {
        public SunStrikeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SunStrikeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}