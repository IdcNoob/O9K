namespace O9K.Evader.Abilities.Heroes.Tidehunter.AnchorSmash
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tidehunter_anchor_smash)]
    internal class AnchorSmashBase : EvaderBaseAbility, IEvadable
    {
        public AnchorSmashBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new AnchorSmashEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}