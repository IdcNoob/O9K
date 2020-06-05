namespace O9K.Evader.Abilities.Heroes.VengefulSpirit.NetherSwap
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.vengefulspirit_nether_swap)]
    internal class NetherSwapBase : EvaderBaseAbility, IUsable<DisableAbility>, IEvadable
    {
        public NetherSwapBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new NetherSwapEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}