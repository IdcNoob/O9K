namespace O9K.Core.Entities.Abilities.Base.Types
{
    using Components.Base;

    using Entities.Units;

    public interface IHealthRestore : IActiveAbility
    {
        bool InstantRestore { get; }

        string RestoreModifierName { get; }

        bool RestoresAlly { get; }

        bool RestoresOwner { get; }

        int GetHealthRestore(Unit9 unit);
    }
}