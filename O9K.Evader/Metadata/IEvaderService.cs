namespace O9K.Evader.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [InheritedExport]
    internal interface IEvaderService : IDisposable
    {
        LoadOrder LoadOrder { get; }

        void Activate();
    }
}