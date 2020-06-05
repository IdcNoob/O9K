namespace O9K.Evader.Abilities.Heroes.LoneDruid.EntanglingClaws
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lone_druid_spirit_bear_entangle)]
    internal class EntanglingClawsBase : EvaderBaseAbility, IEvadable
    {
        public EntanglingClawsBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EntanglingClawsEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}