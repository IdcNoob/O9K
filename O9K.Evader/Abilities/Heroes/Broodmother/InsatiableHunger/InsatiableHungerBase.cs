namespace O9K.Evader.Abilities.Heroes.Broodmother.InsatiableHunger
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.broodmother_insatiable_hunger)]
    internal class InsatiableHungerBase : EvaderBaseAbility, IEvadable
    {
        public InsatiableHungerBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new InsatiableHungerEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}