namespace O9K.Evader.Abilities.Heroes.Riki.SmokeScreen
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.riki_smoke_screen)]
    internal class SmokeScreenBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public SmokeScreenBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SmokeScreenEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}