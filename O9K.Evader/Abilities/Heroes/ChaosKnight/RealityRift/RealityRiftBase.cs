namespace O9K.Evader.Abilities.Heroes.ChaosKnight.RealityRift
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.chaos_knight_reality_rift)]
    internal class RealityRiftBase : EvaderBaseAbility, IEvadable
    {
        public RealityRiftBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RealityRiftEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}