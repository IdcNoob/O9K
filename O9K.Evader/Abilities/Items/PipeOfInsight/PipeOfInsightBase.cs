namespace O9K.Evader.Abilities.Items.PipeOfInsight
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_pipe)]
    internal class PipeOfInsightBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public PipeOfInsightBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}