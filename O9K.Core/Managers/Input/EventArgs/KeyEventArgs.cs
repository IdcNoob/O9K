namespace O9K.Core.Managers.Input.EventArgs
{
    using System;
    using System.Windows.Input;

    using Ensage;

    public sealed class KeyEventArgs : EventArgs
    {
        public KeyEventArgs(WndEventArgs args)
        {
            this.Key = KeyInterop.KeyFromVirtualKey((int)args.WParam);
            this.Process = args.Process;
        }

        public Key Key { get; }

        public bool Process { get; set; }
    }
}