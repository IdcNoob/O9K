namespace O9K.Core.Managers.Menu.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Renderer;

    using EventArgs;

    using Extensions;

    using Logger;

    using Newtonsoft.Json.Linq;

    using SharpDX;

    using Color = System.Drawing.Color;

    public class MenuSelector : MenuSelector<string>
    {
        public MenuSelector(string displayName, IEnumerable<string> items = null, bool heroUnique = false)
            : base(displayName, items, heroUnique)
        {
        }

        public MenuSelector(string displayName, string name, IEnumerable<string> items = null, bool heroUnique = false)
            : base(displayName, name, items, heroUnique)
        {
        }
    }

    public class MenuSelector<T> : MenuItem
    {
        private readonly List<T> items = new List<T>();

        private readonly Dictionary<Lang, Dictionary<T, string>> localizedValues = new Dictionary<Lang, Dictionary<T, string>>();

        private float maxWidth;

        private T selected;

        private EventHandler<SelectorEventArgs<T>> valueChange;

        private string valueText;

        private Vector2 valueTextSize;

        public MenuSelector(string displayName, IEnumerable<T> items = null, bool heroUnique = false)
            : this(displayName, displayName, items, heroUnique)
        {
        }

        public MenuSelector(string displayName, string name, IEnumerable<T> items = null, bool heroUnique = false)
            : base(displayName, name, heroUnique)
        {
            if (items == null)
            {
                return;
            }

            this.items = items.Distinct().ToList();
            if (this.items.Count > 0)
            {
                this.selected = this.items[0];
            }
        }

        public event EventHandler<SelectorEventArgs<T>> ValueChange
        {
            add
            {
                if (!string.IsNullOrEmpty(this.selected.ToString()))
                {
                    value(this, new SelectorEventArgs<T>(this.selected, this.selected));
                }

                this.valueChange += value;
            }
            remove
            {
                this.valueChange -= value;
            }
        }

        public T Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                if (!this.items.Contains(value))
                {
                    return;
                }

                if (this.selected.Equals(value))
                {
                    return;
                }

                this.valueChange?.Invoke(this, new SelectorEventArgs<T>(value, this.selected));
                this.selected = value;

                if (this.SizeCalculated)
                {
                    this.valueText = this.GetLocalizedValueText();
                    this.valueTextSize = this.Renderer.MeasureText(this.valueText, this.MenuStyle.TextSize, this.MenuStyle.Font);
                }
            }
        }

        public static implicit operator T(MenuSelector<T> item)
        {
            return item.Selected;
        }

        public void AddItem(T name)
        {
            if (this.items.Contains(name))
            {
                return;
            }

            this.items.Add(name);

            if (string.IsNullOrEmpty(this.selected?.ToString()))
            {
                this.Selected = name;
            }

            if (this.SizeCalculated)
            {
                this.CalculateFullWidth(name);
            }
        }

        public void AddValuesTranslation(Lang language, IEnumerable<string> localized)
        {
            this.localizedValues[language] = this.items.Zip(
                    localized,
                    (k, v) => new
                    {
                        k,
                        v
                    })
                .ToDictionary(x => x.k, x => x.v);
            this.valueText = this.GetLocalizedValueText();
        }

        public T SetTooltip(string tooltip)
        {
            this.LocalizedTooltip[Lang.En] = tooltip;
            return this;
        }

        internal override void CalculateSize()
        {
            base.CalculateSize();
            this.Size = new Vector2(this.maxWidth, this.MenuStyle.Height);
        }

        internal override object GetSaveValue()
        {
            if (this.items.Count == 0 || this.items[0].Equals(this.Selected))
            {
                return null;
            }

            return this.Selected;
        }

        internal override void Load(JToken token)
        {
            try
            {
                token = token?[this.Name];
                if (token == null)
                {
                    return;
                }

                var value = token.ToObject<T>();
                if (this.items.Contains(value))
                {
                    this.Selected = value;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        internal override bool OnMouseRelease(Vector2 position)
        {
            if (this.items.Count == 0)
            {
                return false;
            }

            var index = this.items.FindIndex(x => x.ToString() == this.selected.ToString());

            if (index == this.items.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            this.Selected = this.items[index];
            return true;
        }

        internal override bool OnMouseWheel(Vector2 position, bool up)
        {
            if (this.items.Count == 0)
            {
                return false;
            }

            var index = this.items.FindIndex(x => x.ToString() == this.selected.ToString());

            if (up)
            {
                if (index == this.items.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            else
            {
                if (index == 0)
                {
                    index = this.items.Count - 1;
                }
                else
                {
                    index--;
                }
            }

            this.Selected = this.items[index];
            return true;
        }

        internal override void SetRenderer(IRenderManager renderer)
        {
            base.SetRenderer(renderer);

            if (!string.IsNullOrEmpty(this.selected?.ToString()))
            {
                this.valueText = this.GetLocalizedValueText();
                this.valueTextSize = this.Renderer.MeasureText(this.valueText, this.MenuStyle.TextSize, this.MenuStyle.Font);
                foreach (var item in this.items)
                {
                    this.CalculateFullWidth(item);
                }
            }
        }

        protected override void Draw(IRenderer renderer)
        {
            base.Draw(renderer);

            if (string.IsNullOrEmpty(this.selected?.ToString()))
            {
                return;
            }

            //value text
            var valuePosition = new Vector2(
                (this.Position.X + this.Size.X) - (this.MenuStyle.RightIndent * 1.5f) - this.valueTextSize.X
                - this.MenuStyle.TextureArrowSize,
                this.Position.Y + ((this.Size.Y - this.MenuStyle.TextSize) / 3.3f));

            renderer.DrawText(valuePosition, this.valueText, Color.White, this.MenuStyle.TextSize, this.MenuStyle.Font);

            //arrow
            var size = this.MenuStyle.TextureArrowSize * 1.4f;
            renderer.DrawTexture(
                this.MenuStyle.TextureLeftArrowKey,
                new RectangleF(
                    (this.Position.X + this.Size.X) - size - (this.MenuStyle.RightIndent * 0.7f),
                    this.Position.Y + ((this.Size.Y - size) / 2f),
                    size,
                    size),
                (float)(Math.PI / 2));
        }

        private void CalculateFullWidth(T name)
        {
            var menuDisplayName = name.ToString().ToSentenceCase();
            var textSize = this.Renderer.MeasureText(menuDisplayName, this.MenuStyle.TextSize, this.MenuStyle.Font);
            var width = this.DisplayNameSize.X + this.MenuStyle.LeftIndent + this.MenuStyle.RightIndent + 10
                        + (this.MenuStyle.TextureArrowSize * 4) + (textSize.X * 1.2f) + this.valueTextSize.X;

            if (width > this.maxWidth)
            {
                this.maxWidth = width;
                this.Size = new Vector2(this.maxWidth, this.MenuStyle.Height);
                this.ParentMenu.CalculateWidth();
            }
        }

        private string GetLocalizedValueText()
        {
            if (!this.localizedValues.TryGetValue(this.Language, out var values))
            {
                return this.selected.ToString().ToSentenceCase();
            }

            return values[this.selected];
        }
    }
}