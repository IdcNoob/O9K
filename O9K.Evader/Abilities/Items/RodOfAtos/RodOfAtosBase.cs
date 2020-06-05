namespace O9K.Evader.Abilities.Items.RodOfAtos
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_rod_of_atos)]
    internal class RodOfAtosBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public RodOfAtosBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RodOfAtosEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}