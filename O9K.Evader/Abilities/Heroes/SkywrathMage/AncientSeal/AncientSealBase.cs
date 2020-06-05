namespace O9K.Evader.Abilities.Heroes.SkywrathMage.AncientSeal
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.skywrath_mage_ancient_seal)]
    internal class AncientSealBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public AncientSealBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new AncientSealEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}