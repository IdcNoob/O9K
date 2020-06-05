namespace O9K.Evader.Abilities.Heroes.DarkWillow.Terrorize
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dark_willow_terrorize)]
    internal class TerrorizeBase : EvaderBaseAbility, IEvadable
    {
        public TerrorizeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TerrorizeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}