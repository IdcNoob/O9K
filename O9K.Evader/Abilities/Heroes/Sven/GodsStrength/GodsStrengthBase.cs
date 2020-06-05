namespace O9K.Evader.Abilities.Heroes.Sven.GodsStrength
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.sven_gods_strength)]
    internal class GodsStrengthBase : EvaderBaseAbility, IEvadable
    {
        public GodsStrengthBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GodsStrengthEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}