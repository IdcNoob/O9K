namespace O9K.Evader.Abilities.Heroes.Magnus.Empower
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.magnataur_empower)]
    internal class EmpowerBase : EvaderBaseAbility, IEvadable
    {
        public EmpowerBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EmpowerEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}