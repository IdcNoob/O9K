namespace O9K.Evader.Abilities.Items.ArmletOfMordiggian
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_armlet)]
    internal class ArmletOfMordiggianBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public ArmletOfMordiggianBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new ArmletOfMordiggianUsable(this.Ability, this.Menu);
        }
    }
}