namespace O9K.Evader.Abilities.Items.LotusOrb
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_lotus_orb)]
    internal class LotusOrbBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public LotusOrbBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new LotusOrbUsable(this.Ability, this.Menu);
        }
    }
}