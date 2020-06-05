namespace O9K.Evader.Abilities.Heroes.Invoker.ChaosMeteor
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.invoker_chaos_meteor)]
    internal class ChaosMeteorBase : EvaderBaseAbility, IEvadable
    {
        public ChaosMeteorBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ChaosMeteorEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}