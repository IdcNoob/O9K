namespace O9K.Core.Entities.Abilities.Base.Components
{
    public interface IHasHealthCost
    {
        bool CanSuicide { get; }

        int HealthCost { get; }
    }
}