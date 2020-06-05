namespace O9K.Core.Entities.Abilities.Base.Components
{
    using Ensage;

    public interface IUpgradable
    {
        AbilityId UpgradedBy { get; }
    }
}