namespace O9K.Evader.Abilities.Heroes.Snapfire.MortimerKisses
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.snapfire_mortimer_kisses)]
    internal class MortimerKissesBase : EvaderBaseAbility, IEvadable
    {
        public MortimerKissesBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MortimerKissesEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}