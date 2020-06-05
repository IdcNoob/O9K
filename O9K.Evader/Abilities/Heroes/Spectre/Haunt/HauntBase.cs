namespace O9K.Evader.Abilities.Heroes.Spectre.Haunt
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.spectre_haunt)]
    internal class HauntBase : EvaderBaseAbility, IEvadable
    {
        public HauntBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new HauntEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}