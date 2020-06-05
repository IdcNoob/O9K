namespace O9K.Evader.Abilities.Heroes.Juggernaut.Omnislash
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.juggernaut_omni_slash)]
    internal class OmnislashBase : EvaderBaseAbility, IEvadable
    {
        public OmnislashBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new OmnislashEvadabe(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}