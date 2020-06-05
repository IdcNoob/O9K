namespace O9K.ItemManager.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [InheritedExport]
    internal interface IModule : IDisposable
    {
        void Activate();
    }
}