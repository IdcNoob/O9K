namespace O9K.Evader.Abilities.Heroes.Mars.ArenaOfBlood
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.mars_arena_of_blood)]
    internal class ArenaOfBloodBase : EvaderBaseAbility, IEvadable
    {
        public ArenaOfBloodBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ArenaOfBloodEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}