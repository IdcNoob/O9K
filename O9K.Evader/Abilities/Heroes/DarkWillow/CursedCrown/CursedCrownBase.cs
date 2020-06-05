namespace O9K.Evader.Abilities.Heroes.DarkWillow.CursedCrown
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dark_willow_cursed_crown)]
    internal class CursedCrownBase : EvaderBaseAbility, IEvadable
    {
        public CursedCrownBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new CursedCrownEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}