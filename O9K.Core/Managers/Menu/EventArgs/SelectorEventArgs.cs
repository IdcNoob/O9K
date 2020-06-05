namespace O9K.Core.Managers.Menu.EventArgs
{
    public sealed class SelectorEventArgs<T> : MenuEventArgs<T>
    {
        public SelectorEventArgs(T newValue, T oldValue)
            : base(newValue, oldValue)
        {
        }
    }
}