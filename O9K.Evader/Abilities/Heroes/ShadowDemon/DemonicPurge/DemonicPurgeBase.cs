namespace O9K.Evader.Abilities.Heroes.ShadowDemon.DemonicPurge
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.shadow_demon_demonic_purge)]
    internal class DemonicPurgeBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public DemonicPurgeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DemonicPurgeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}