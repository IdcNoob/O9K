namespace O9K.Evader.Abilities.Heroes.ShadowShaman.Shackles
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.shadow_shaman_shackles)]
    internal class ShacklesBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public ShacklesBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShacklesEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}