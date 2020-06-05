namespace O9K.Evader.Abilities.Items.AbyssalBlade
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_abyssal_blade)]
    internal class AbyssalBladeBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public AbyssalBladeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new AbyssalBladeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}