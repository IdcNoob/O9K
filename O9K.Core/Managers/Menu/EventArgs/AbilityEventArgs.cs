namespace O9K.Core.Managers.Menu.EventArgs
{
    public sealed class AbilityEventArgs : MenuEventArgs<bool>
    {
        public AbilityEventArgs(string ability, bool newValue, bool oldValue)
            : base(newValue, oldValue)
        {
            this.Ability = ability;
        }

        public string Ability { get; }
    }
}