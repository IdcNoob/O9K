namespace O9K.Evader.Abilities.Items.EtherealBlade
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_ethereal_blade)]
    internal class EtherealBladeBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>, IUsable<DisableAbility>
    {
        public EtherealBladeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EtherealBladeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        CounterAbility IUsable<CounterAbility>.GetUsableAbility()
        {
            return new EtherealBladeUsableCounter(this.Ability, this.Menu);
        }

        DisableAbility IUsable<DisableAbility>.GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}