namespace O9K.Evader.Abilities.Heroes.StormSpirit.StaticRemnant
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.storm_spirit_static_remnant)]
    internal class StaticRemnantBase : EvaderBaseAbility, IEvadable
    {
        public StaticRemnantBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new StaticRemnantEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}