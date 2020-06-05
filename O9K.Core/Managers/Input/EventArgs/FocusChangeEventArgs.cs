namespace O9K.Core.Managers.Input.EventArgs
{
    using System;

    using Ensage;

    public class FocusChangeEventArgs : EventArgs
    {
        public FocusChangeEventArgs(WndEventArgs args)
        {
            this.Active = args.WParam != 0;
        }

        public bool Active { get; }
    }
}