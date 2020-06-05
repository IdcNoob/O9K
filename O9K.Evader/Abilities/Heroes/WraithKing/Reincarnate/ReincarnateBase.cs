namespace O9K.Evader.Abilities.Heroes.WraithKing.Reincarnate
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.skeleton_king_reincarnation)]
    internal class ReincarnateBase : EvaderBaseAbility, IEvadable
    {
        public ReincarnateBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ReincarnateEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}