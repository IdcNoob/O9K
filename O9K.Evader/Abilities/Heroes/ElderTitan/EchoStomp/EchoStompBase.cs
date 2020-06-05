namespace O9K.Evader.Abilities.Heroes.ElderTitan.EchoStomp
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.elder_titan_echo_stomp)]
    internal class EchoStompBase : EvaderBaseAbility, IEvadable
    {
        public EchoStompBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EchoStompEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}