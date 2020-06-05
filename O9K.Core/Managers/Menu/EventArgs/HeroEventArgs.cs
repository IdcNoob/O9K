namespace O9K.Core.Managers.Menu.EventArgs
{
    public sealed class HeroEventArgs : MenuEventArgs<bool>
    {
        public HeroEventArgs(string hero, bool newValue, bool oldValue)
            : base(newValue, oldValue)
        {
            this.Hero = hero;
        }

        public string Hero { get; }
    }
}