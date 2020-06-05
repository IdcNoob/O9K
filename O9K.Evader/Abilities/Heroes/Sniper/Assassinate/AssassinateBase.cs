namespace O9K.Evader.Abilities.Heroes.Sniper.Assassinate
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.sniper_assassinate)]
    internal class AssassinateBase : EvaderBaseAbility, IEvadable
    {
        public AssassinateBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new AssassinateEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}