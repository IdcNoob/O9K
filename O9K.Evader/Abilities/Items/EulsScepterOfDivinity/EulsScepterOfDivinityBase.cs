namespace O9K.Evader.Abilities.Items.EulsScepterOfDivinity
{
    using Base;
    using Base.Usable.CounterAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_cyclone)]
    internal class EulsScepterOfDivinityBase : EvaderBaseAbility, IUsable<CounterAbility>, IUsable<DisableAbility>
    {
        public EulsScepterOfDivinityBase(Ability9 ability)
            : base(ability)
        {
        }

        CounterAbility IUsable<CounterAbility>.GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }

        DisableAbility IUsable<DisableAbility>.GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}