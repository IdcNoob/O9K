namespace O9K.Evader.Abilities.Items.SilverEdge
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DodgeAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_silver_edge)]
    internal class SilverEdgeBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>, IUsable<DodgeAbility>
    {
        public SilverEdgeBase(Ability9 ability)
            : base(ability)
        {
            //todo add projectile evadable? 
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SilverEdgeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        CounterAbility IUsable<CounterAbility>.GetUsableAbility()
        {
            return new CounterInvisibilityAbility(this.Ability, this.Menu);
        }

        DodgeAbility IUsable<DodgeAbility>.GetUsableAbility()
        {
            return new DodgeAbility(this.Ability, this.Menu);
        }
    }
}