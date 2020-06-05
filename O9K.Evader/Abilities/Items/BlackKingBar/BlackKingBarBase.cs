namespace O9K.Evader.Abilities.Items.BlackKingBar
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_black_king_bar)]
    internal class BlackKingBarBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public BlackKingBarBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BlackKingBarEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new BlackKingBarUsable(this.Ability, this.Menu);
        }
    }
}