namespace O9K.Evader.Abilities.Heroes.DarkSeer.Surge
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dark_seer_surge)]
    internal class SurgeBase : EvaderBaseAbility, IEvadable
    {
        public SurgeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SurgeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}