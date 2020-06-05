namespace O9K.Hud.Modules
{
    using System;
    using System.ComponentModel.Composition;

    [InheritedExport]
    internal interface IHudModule : IDisposable
    {
        void Activate();
    }
}