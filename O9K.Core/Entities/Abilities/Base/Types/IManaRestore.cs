namespace O9K.Core.Entities.Abilities.Base.Types
{
    using Components.Base;

    using Entities.Units;

    public interface IManaRestore : IActiveAbility
    {
        bool InstantRestore { get; }

        string RestoreModifierName { get; }

        bool RestoresAlly { get; }

        bool RestoresOwner { get; }

        int GetManaRestore(Unit9 unit);
    }
}