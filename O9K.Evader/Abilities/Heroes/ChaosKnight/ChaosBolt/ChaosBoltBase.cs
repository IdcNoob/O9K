namespace O9K.Evader.Abilities.Heroes.ChaosKnight.ChaosBolt
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.chaos_knight_chaos_bolt)]
    internal class ChaosBoltBase : EvaderBaseAbility, IEvadable
    {
        public ChaosBoltBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ChaosBoltEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}