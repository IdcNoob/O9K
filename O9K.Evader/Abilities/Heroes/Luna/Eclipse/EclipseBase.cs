namespace O9K.Evader.Abilities.Heroes.Luna.Eclipse
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.luna_eclipse)]
    internal class EclipseBase : EvaderBaseAbility, IEvadable
    {
        public EclipseBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EclipseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}