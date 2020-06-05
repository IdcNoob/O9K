namespace O9K.Evader.Abilities.Heroes.Warlock.Upheaval
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.warlock_upheaval)]
    internal class UpheavalBase : EvaderBaseAbility, IEvadable
    {
        public UpheavalBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new UpheavalEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}