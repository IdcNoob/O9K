namespace O9K.Evader.Abilities.Heroes.ShadowFiend.RequiemOfSouls
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.nevermore_requiem)]
    internal class RequiemOfSoulsBase : EvaderBaseAbility, IEvadable
    {
        public RequiemOfSoulsBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RequiemOfSoulsEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}