namespace O9K.Evader.Abilities.Heroes.MonkeyKing.Mischief
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.monkey_king_mischief)]
    internal class MischiefBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public MischiefBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new MischiefUsable(this.Ability, this.Menu);
        }
    }
}