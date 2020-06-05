namespace O9K.Evader.Abilities.Heroes.Undying.FleshGolem
{
    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.undying_flesh_golem)]
    internal class FleshGolemBase : EvaderBaseAbility
    {
        public FleshGolemBase(Ability9 ability)
            : base(ability)
        {
            //todo evadable ?
        }
    }
}