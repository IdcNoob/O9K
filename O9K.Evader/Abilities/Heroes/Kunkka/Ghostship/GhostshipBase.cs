namespace O9K.Evader.Abilities.Heroes.Kunkka.Ghostship
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.kunkka_ghostship)]
    internal class GhostshipBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public GhostshipBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GhostshipEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}