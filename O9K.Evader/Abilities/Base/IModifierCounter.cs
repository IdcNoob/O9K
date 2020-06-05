namespace O9K.Evader.Abilities.Base
{
    using System.Collections.Generic;

    using Core.Entities.Units;
    using Core.Managers.Menu.Items;

    using Ensage;

    internal interface IModifierCounter
    {
        bool ModifierAllyCounter { get; }

        HashSet<AbilityId> ModifierBlinks { get; }

        MenuSwitcher ModifierCounterEnabled { get; }

        HashSet<AbilityId> ModifierCounters { get; }

        HashSet<AbilityId> ModifierDisables { get; }

        bool ModifierEnemyCounter { get; }

        Unit9 Owner { get; }

        void AddModifier(Modifier modifier, Unit9 modifierOwner);
    }
}