namespace O9K.Evader.Abilities.Heroes.ShadowDemon.SoulCatcher
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.shadow_demon_soul_catcher)]
    internal class SoulCatcherBase : EvaderBaseAbility, IEvadable
    {
        public SoulCatcherBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SoulCatcherEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}