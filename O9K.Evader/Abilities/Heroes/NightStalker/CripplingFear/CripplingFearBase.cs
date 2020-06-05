namespace O9K.Evader.Abilities.Heroes.NightStalker.CripplingFear
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.night_stalker_crippling_fear)]
    internal class CripplingFearBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public CripplingFearBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new CripplingFearEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}