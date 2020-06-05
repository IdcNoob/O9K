namespace O9K.Core.Managers.Menu.Items
{
    using System;

    using Ensage.SDK.Renderer;

    using EventArgs;

    using Logger;

    using Newtonsoft.Json.Linq;

    using SharpDX;

    using Color = System.Drawing.Color;

    public class MenuSwitcher : MenuItem
    {
        private readonly bool defaultValue;

        private bool isEnabled;

        private bool loaded;

        private EventHandler<SwitcherEventArgs> valueChange;

        public MenuSwitcher(string displayName, bool defaultValue = true, bool heroUnique = false)
            : this(displayName, displayName, defaultValue, heroUnique)
        {
        }

        public MenuSwitcher(string displayName, string name, bool defaultValue = true, bool heroUnique = false)
            : base(displayName, name, heroUnique)
        {
            this.defaultValue = defaultValue;
            this.isEnabled = defaultValue;
        }

        public event EventHandler<SwitcherEventArgs> ValueChange
        {
            add
            {
                if (this.loaded && this.IsEnabled)
                {
                    value(this, new SwitcherEventArgs(this.IsEnabled, this.IsEnabled));
                }

                this.valueChange += value;
            }
            remove
            {
                this.valueChange -= value;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (value == this.isEnabled)
                {
                    return;
                }

                var args = new SwitcherEventArgs(value, this.isEnabled);
                this.valueChange?.Invoke(this, args);

                if (args.Process)
                {
                    this.isEnabled = value;
                }
            }
        }

        public static implicit operator bool(MenuSwitcher item)
        {
            return item.isEnabled;
        }

        public MenuSwitcher SetTooltip(string tooltip)
        {
            this.LocalizedTooltip[Lang.En] = tooltip;
            return this;
        }

        internal override void CalculateSize()
        {
            base.CalculateSize();
            this.Size = new Vector2(this.Size.X + this.MenuStyle.TextureArrowSize + 10, this.Size.Y);
        }

        internal override object GetSaveValue()
        {
            if (!this.SaveValue)
            {
                return this.defaultValue;
            }

            if (this.defaultValue == this.isEnabled)
            {
                return null;
            }

            return this.IsEnabled;
        }

        internal override void Load(JToken token)
        {
            try
            {
                token = token?[this.Name];
                if (token != null)
                {
                    this.isEnabled = token.ToObject<bool>();
                }

                if (this.isEnabled)
                {
                    this.isEnabled = false;
                    this.IsEnabled = true; // invoke event
                }

                this.loaded = true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        internal override bool OnMouseRelease(Vector2 position)
        {
            this.IsEnabled = !this.IsEnabled;
            return true;
        }

        protected override void Draw(IRenderer renderer)
        {
            base.Draw(renderer);

            //rectangle
            renderer.DrawFilledRectangle(
                new RectangleF(
                    (this.Position.X + this.Size.X) - this.MenuStyle.TextureArrowSize - this.MenuStyle.RightIndent,
                    this.Position.Y + ((this.Size.Y - this.MenuStyle.TextureArrowSize) / 2.2f),
                    this.MenuStyle.TextureArrowSize,
                    this.MenuStyle.TextureArrowSize),
                this.IsEnabled ? Color.White : this.MenuStyle.BackgroundColor,
                Color.FromArgb(255, 50, 50, 50),
                2);
        }
    }
}