namespace O9K.Core.Managers.Menu.EventArgs
{
    public sealed class SwitcherEventArgs : MenuEventArgs<bool>
    {
        public SwitcherEventArgs(bool newValue, bool oldValue)
            : base(newValue, oldValue)
        {
        }

        public bool Process { get; set; } = true;
    }
}