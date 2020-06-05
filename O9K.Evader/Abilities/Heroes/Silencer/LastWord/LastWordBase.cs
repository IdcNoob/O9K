namespace O9K.Evader.Abilities.Heroes.Silencer.LastWord
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.silencer_last_word)]
    internal class LastWordBase : EvaderBaseAbility, IEvadable
    {
        public LastWordBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new LastWordEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}