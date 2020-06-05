namespace O9K.Evader.Abilities.Items.MantaStyle
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_manta)]
    internal class MantaStyleBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public MantaStyleBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new MantaStyleUsable(this.Ability, this.Menu);
        }
    }
}