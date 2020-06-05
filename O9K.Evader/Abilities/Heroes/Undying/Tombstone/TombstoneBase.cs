namespace O9K.Evader.Abilities.Heroes.Undying.Tombstone
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.undying_tombstone)]
    internal class TombstoneBase : EvaderBaseAbility, IEvadable
    {
        public TombstoneBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TombstoneEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}