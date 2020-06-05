namespace O9K.Evader.Abilities.Heroes.Enigma.BlackHole
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.enigma_black_hole)]
    internal class BlackHoleBase : EvaderBaseAbility, IEvadable
    {
        public BlackHoleBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BlackHoleEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}