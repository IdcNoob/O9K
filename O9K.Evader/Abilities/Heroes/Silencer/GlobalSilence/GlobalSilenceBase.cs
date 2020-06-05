namespace O9K.Evader.Abilities.Heroes.Silencer.GlobalSilence
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.silencer_global_silence)]
    internal class GlobalSilenceBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public GlobalSilenceBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GlobalSilenceEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}