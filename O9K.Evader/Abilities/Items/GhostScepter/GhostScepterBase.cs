namespace O9K.Evader.Abilities.Items.GhostScepter
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_ghost)]
    internal class GhostScepterBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public GhostScepterBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GhostScepterEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}