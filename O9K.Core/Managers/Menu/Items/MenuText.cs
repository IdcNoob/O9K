namespace O9K.Core.Managers.Menu.Items
{
    using Newtonsoft.Json.Linq;

    public class MenuText : MenuItem
    {
        public MenuText(string displayName)
            : base(displayName, displayName)
        {
        }

        public MenuText(string displayName, string name)
            : base(displayName, name)
        {
        }

        public MenuText SetTooltip(string tooltip)
        {
            this.LocalizedTooltip[Lang.En] = tooltip;
            return this;
        }

        internal override object GetSaveValue()
        {
            return null;
        }

        internal override void Load(JToken token)
        {
        }
    }
}