namespace O9K.Core.Entities.Abilities.Base.Types
{
    using Components.Base;

    using Ensage;

    public interface IShield : IActiveAbility
    {
        UnitState AppliesUnitState { get; }

        string ShieldModifierName { get; }

        bool ShieldsAlly { get; }

        bool ShieldsOwner { get; }
    }
}