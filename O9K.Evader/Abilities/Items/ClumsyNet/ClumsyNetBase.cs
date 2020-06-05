namespace O9K.Evader.Abilities.Items.ClumsyNet
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_clumsy_net)]
    internal class ClumsyNetBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public ClumsyNetBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ClumsyNetEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}