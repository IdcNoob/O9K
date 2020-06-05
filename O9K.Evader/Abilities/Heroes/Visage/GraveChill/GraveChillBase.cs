namespace O9K.Evader.Abilities.Heroes.Visage.GraveChill
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.visage_grave_chill)]
    internal class GraveChillBase : EvaderBaseAbility, IEvadable
    {
        public GraveChillBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GraveChillEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}