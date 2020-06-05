namespace O9K.Evader.Abilities.Items.BladeMail
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_blade_mail)]
    internal class BladeMailBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public BladeMailBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BladeMailEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new BladeMailUsable(this.Ability, this.Menu);
        }
    }
}