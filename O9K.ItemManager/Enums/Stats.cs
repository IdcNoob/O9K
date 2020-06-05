namespace O9K.ItemManager.Enums
{
    using System;

    [Flags]
    public enum Stats
    {
        None = 0,

        Health = 1 << 0,

        Mana = 1 << 1,

        All = Health | Mana
    }
}