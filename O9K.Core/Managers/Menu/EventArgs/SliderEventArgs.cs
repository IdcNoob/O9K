namespace O9K.Core.Managers.Menu.EventArgs
{
    public sealed class SliderEventArgs : MenuEventArgs<int>
    {
        public SliderEventArgs(int newValue, int oldValue)
            : base(newValue, oldValue)
        {
        }
    }
}