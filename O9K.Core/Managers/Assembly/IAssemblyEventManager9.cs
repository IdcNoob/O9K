namespace O9K.Core.Managers.Assembly
{
    using System;

    public interface IAssemblyEventManager9
    {
        event EventHandler AutoSoulRingEnabled;

        event EventHandler EvaderPredictedDeath;

        event EventHandler ForceBlockerResubscribe;

        event EventHandler<string> OnAssemblyLoad;

        event EventHandler<bool> OrderBlockerMoveCamera;

        void AssemblyLoaded();

        void InvokeAutoSoulRingEnabled();

        void InvokeEvaderPredictedDeath();

        void InvokeForceBlockerResubscribe();

        void InvokeOrderBlockerMoveCamera(bool enabled);
    }
}