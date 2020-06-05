namespace O9K.Evader.Abilities.Items.HurricanePike
{
    using Base;
    using Base.Usable.BlinkAbility;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_hurricane_pike)]
    internal class HurricanePikeBase : EvaderBaseAbility, IUsable<BlinkAbility>, IUsable<CounterAbility>
    {
        public HurricanePikeBase(Ability9 ability)
            : base(ability)
        {
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkLeapAbility(this.Ability, this.Pathfinder, this.ActionManager, this.Menu);
        }

        CounterAbility IUsable<CounterAbility>.GetUsableAbility()
        {
            return new HurricanePikeUsable(this.Ability, this.Menu);
        }
    }
}