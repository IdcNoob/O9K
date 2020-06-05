namespace O9K.Evader.Abilities.Heroes.Slardar.SlithereenCrush
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.slardar_slithereen_crush)]
    internal class SlithereenCrushBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public SlithereenCrushBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SlithereenCrushEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}