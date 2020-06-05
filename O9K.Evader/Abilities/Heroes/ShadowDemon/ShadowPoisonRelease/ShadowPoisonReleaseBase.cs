namespace O9K.Evader.Abilities.Heroes.ShadowDemon.ShadowPoisonRelease
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.shadow_demon_shadow_poison_release)]
    internal class ShadowPoisonReleaseBase : EvaderBaseAbility //, IEvadable
    {
        public ShadowPoisonReleaseBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShadowPoisonReleaseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}