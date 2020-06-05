namespace O9K.Evader.Abilities.Heroes.Meepo.Poof
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.meepo_poof)]
    internal class PoofBase : EvaderBaseAbility, IEvadable
    {
        public PoofBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PoofEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}