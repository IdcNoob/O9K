namespace O9K.Core.Entities.Abilities.Base.Components
{
    public interface IAppliesImmobility
    {
        string ImmobilityModifierName { get; }

        bool IsValid { get; }
    }
}