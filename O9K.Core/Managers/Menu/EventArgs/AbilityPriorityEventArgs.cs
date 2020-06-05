namespace O9K.Core.Managers.Menu.EventArgs
{
    public sealed class AbilityPriorityEventArgs : MenuEventArgs<int>
    {
        public AbilityPriorityEventArgs(string ability, int newValue, int oldValue)
            : base(newValue, oldValue)
        {
            this.Ability = ability;
        }

        public string Ability { get; }
    }
}