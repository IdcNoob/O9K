namespace O9K.Core.Managers.Assembly
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Reflection;

    [Export(typeof(IAssemblyEventManager9))]
    public sealed class AssemblyEventManager9 : IAssemblyEventManager9
    {
        private readonly List<string> loadedAssemblies = new List<string>();

        public event EventHandler AutoSoulRingEnabled;

        public event EventHandler EvaderPredictedDeath;

        public event EventHandler ForceBlockerResubscribe;

        public event EventHandler<string> OnAssemblyLoad
        {
            add
            {
                this.AssemblyLoad += value;

                foreach (var assembly in this.loadedAssemblies)
                {
                    value(this, assembly);
                }
            }
            remove
            {
                this.AssemblyLoad -= value;
            }
        }

        public event EventHandler<bool> OrderBlockerMoveCamera;

        private event EventHandler<string> AssemblyLoad;

        public void AssemblyLoaded()
        {
            var name = Assembly.GetCallingAssembly().GetName().Name;
            this.loadedAssemblies.Add(name);
            this.AssemblyLoad?.Invoke(this, name);
        }

        public void InvokeAutoSoulRingEnabled()
        {
            this.AutoSoulRingEnabled?.Invoke(this, EventArgs.Empty);
        }

        public void InvokeEvaderPredictedDeath()
        {
            this.EvaderPredictedDeath?.Invoke(this, EventArgs.Empty);
        }

        public void InvokeForceBlockerResubscribe()
        {
            this.ForceBlockerResubscribe?.Invoke(this, EventArgs.Empty);
        }

        public void InvokeOrderBlockerMoveCamera(bool enabled)
        {
            this.OrderBlockerMoveCamera?.Invoke(this, enabled);
        }
    }
}