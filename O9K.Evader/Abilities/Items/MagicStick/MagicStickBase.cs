namespace O9K.Evader.Abilities.Items.MagicStick
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_magic_stick)]
    [AbilityId(AbilityId.item_magic_wand)]
    internal class MagicStickBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public MagicStickBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}