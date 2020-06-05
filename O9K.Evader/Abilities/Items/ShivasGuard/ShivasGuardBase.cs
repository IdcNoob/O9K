namespace O9K.Evader.Abilities.Items.ShivasGuard
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_shivas_guard)]
    internal class ShivasGuardBase : EvaderBaseAbility, /*IEvadable,*/ IUsable<CounterAbility>
    {
        public ShivasGuardBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShivasGuardEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}