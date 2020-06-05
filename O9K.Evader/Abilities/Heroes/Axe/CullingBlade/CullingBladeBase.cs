namespace O9K.Evader.Abilities.Heroes.Axe.CullingBlade
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.axe_culling_blade)]
    internal class CullingBladeBase : EvaderBaseAbility, IEvadable
    {
        public CullingBladeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new CullingBladeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}