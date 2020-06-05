namespace O9K.Evader.Abilities.Heroes.Earthshaker.EchoSlam
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.earthshaker_echo_slam)]
    internal class EchoSlamBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public EchoSlamBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EchoSlamEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}