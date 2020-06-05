namespace O9K.Core.Managers.Menu.EventArgs
{
    using System;

    public abstract class MenuEventArgs<T> : EventArgs
    {
        protected MenuEventArgs(T newValue, T oldValue)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
        }

        public T NewValue { get; }

        public T OldValue { get; }
    }
}