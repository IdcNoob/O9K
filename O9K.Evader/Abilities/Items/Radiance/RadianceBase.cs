namespace O9K.Evader.Abilities.Items.Radiance
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_radiance)]
    internal class RadianceBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public RadianceBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new RadianceUsable(this.Ability, this.Menu);
        }
    }
}