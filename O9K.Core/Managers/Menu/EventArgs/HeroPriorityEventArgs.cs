namespace O9K.Core.Managers.Menu.EventArgs
{
    public sealed class HeroPriorityEventArgs : MenuEventArgs<int>
    {
        public HeroPriorityEventArgs(string hero, int newValue, int oldValue)
            : base(newValue, oldValue)
        {
            this.Hero = hero;
        }

        public string Hero { get; }
    }
}