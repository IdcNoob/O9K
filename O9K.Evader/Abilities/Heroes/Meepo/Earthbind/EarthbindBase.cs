namespace O9K.Evader.Abilities.Heroes.Meepo.Earthbind
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.meepo_earthbind)]
    internal class EarthbindBase : EvaderBaseAbility, IEvadable
    {
        public EarthbindBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EarthbindEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}