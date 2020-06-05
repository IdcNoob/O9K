namespace O9K.Evader.Abilities.Heroes.Puck.WaningRift
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.puck_waning_rift)]
    internal class WaningRiftBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public WaningRiftBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WaningRiftEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}