namespace O9K.Evader.Abilities.Items.CrimsonGuard
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_crimson_guard)]
    internal class CrimsonGuardBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public CrimsonGuardBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}