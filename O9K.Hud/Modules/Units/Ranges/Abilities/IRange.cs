namespace O9K.Hud.Modules.Units.Ranges.Abilities
{
    using System;

    using Core.Managers.Menu.Items;

    internal interface IRange : IDisposable
    {
        uint Handle { get; }

        bool IsEnabled { get; }

        string Name { get; }

        void Enable(Menu heroMenu);

        void UpdateRange();
    }
}