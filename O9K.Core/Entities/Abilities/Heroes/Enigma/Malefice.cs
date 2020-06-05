namespace O9K.Core.Entities.Abilities.Heroes.Enigma
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.enigma_malefice)]
    public class Malefice : RangedAbility, IDisable
    {
        public Malefice(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}