namespace O9K.Evader.Abilities.Heroes.Jakiro.IcePath
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.jakiro_ice_path)]
    internal class IcePathBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public IcePathBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new IcePathEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}