namespace O9K.Evader.Abilities.Heroes.CrystalMaiden.FreezingField
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.crystal_maiden_freezing_field)]
    internal class FreezingFieldBase : EvaderBaseAbility, IEvadable
    {
        public FreezingFieldBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FreezingFieldEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}