namespace O9K.Core.Managers.Menu.EventArgs
{
    public sealed class KeyEventArgs : MenuEventArgs<bool>
    {
        public KeyEventArgs(bool newValue, bool oldValue)
            : base(newValue, oldValue)
        {
        }
    }
}