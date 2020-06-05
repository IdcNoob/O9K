namespace O9K.Core.Managers.Menu.Items
{
    using System;
    using System.Collections.Generic;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Input;

    using Logger;

    using Newtonsoft.Json.Linq;

    using SharpDX;

    using Color = System.Drawing.Color;

    public abstract class MenuItem : IEquatable<MenuItem>
    {
        private static readonly int heroId = (int)ObjectManager.LocalPlayer.SelectedHeroId;

        protected Dictionary<Lang, string> LocalizedTooltip = new Dictionary<Lang, string>();

        private readonly Dictionary<Lang, string> localizedName = new Dictionary<Lang, string>();

        private float hooverTime;

        private bool isHoovered;

        private bool isVisible;

        protected MenuItem(string displayName, string name, bool heroUnique = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (heroUnique)
            {
                this.Name = name + heroId;
            }
            else
            {
                this.Name = name;
            }

            this.localizedName[Lang.En] = displayName;
        }

        public string DisplayName
        {
            get
            {
                if (this.localizedName.TryGetValue(this.Language, out var name))
                {
                    return name;
                }

                return this.localizedName[Lang.En];
            }
        }

        public virtual bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            internal set
            {
                if (this.DisableDrawing)
                {
                    return;
                }

                this.isVisible = value;
            }
        }

        public string Name { get; }

        public Menu ParentMenu { get; internal set; }

        public string Tooltip
        {
            get
            {
                if (this.LocalizedTooltip.Count == 0)
                {
                    return null;
                }

                if (this.LocalizedTooltip.TryGetValue(this.Language, out var tooltip))
                {
                    return tooltip;
                }

                return this.LocalizedTooltip[Lang.En];
            }
        }

        internal bool IsMainMenu { get; set; }

        internal Vector2 Size { get; set; }

        internal bool SizeCalculated { get; set; }

        protected bool DisableDrawing { get; private set; }

        protected Vector2 DisplayNameSize { get; set; }

        protected IInputManager9 InputManager { get; set; }

        protected Lang Language { get; set; }

        protected MenuStyle MenuStyle { get; set; }

        protected Vector2 Position { get; set; }

        protected IRenderManager Renderer { get; set; }

        protected bool SaveValue { get; private set; } = true;

        protected int TextIndent { get; set; }

        public void AddTooltipTranslation(Lang language, string name)
        {
            this.LocalizedTooltip[language] = name;
        }

        public void AddTranslation(Lang language, string name)
        {
            this.localizedName[language] = name;

            if (this.Renderer != null)
            {
                this.CalculateSize();
            }
        }

        public void DisableSave()
        {
            this.SaveValue = false;
        }

        public bool Equals(MenuItem other)
        {
            return this.Name == other?.Name;
        }

        public void Hide()
        {
            this.IsVisible = false;
            this.DisableDrawing = true;
        }

        internal virtual void CalculateSize()
        {
            this.DisplayNameSize = this.Renderer.MeasureText(this.DisplayName, this.MenuStyle.TextSize, this.MenuStyle.Font);
            var width = this.DisplayNameSize.X + this.MenuStyle.LeftIndent + this.MenuStyle.RightIndent
                        + (this.MenuStyle.TextureArrowSize * 2) + this.TextIndent;
            this.Size = new Vector2(width, this.MenuStyle.Height);
            this.SizeCalculated = true;
        }

        internal virtual MenuItem GetItemUnder(Vector2 position)
        {
            if (!this.IsVisible)
            {
                return null;
            }

            if (position.X >= this.Position.X && position.X <= this.Position.X + this.Size.X && position.Y >= this.Position.Y
                && position.Y <= this.Position.Y + this.Size.Y)
            {
                return this;
            }

            return null;
        }

        internal abstract object GetSaveValue();

        internal void HooverEnd()
        {
            this.isHoovered = false;

            if (this is Menu menu && !menu.IsCollapsed)
            {
                return;
            }

            this.hooverTime = Game.RawGameTime;
        }

        internal void HooverStart()
        {
            this.isHoovered = true;

            if (this is Menu menu && !menu.IsCollapsed)
            {
                return;
            }

            this.hooverTime = Game.RawGameTime;
        }

        internal abstract void Load(JToken token);

        internal void OnDraw(IRenderer renderer, Vector2 position, float childWidth)
        {
            if (!this.IsVisible)
            {
                return;
            }

            this.Position = position;
            this.Size = new Vector2(childWidth, this.MenuStyle.Height);

            try
            {
                this.Draw(renderer);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        internal virtual bool OnMousePress(Vector2 position)
        {
            return false;
        }

        internal virtual bool OnMouseRelease(Vector2 position)
        {
            return false;
        }

        internal virtual bool OnMouseWheel(Vector2 position, bool up)
        {
            return false;
        }

        internal virtual void Remove()
        {
        }

        internal virtual void SetInputManager(IInputManager9 inputManager)
        {
            this.InputManager = inputManager;
        }

        internal virtual void SetLanguage(Lang language)
        {
            this.Language = language;
        }

        internal virtual void SetRenderer(IRenderManager renderer)
        {
            this.Renderer = renderer;
        }

        internal virtual void SetStyle(MenuStyle menuStyle)
        {
            this.MenuStyle = menuStyle;
        }

        protected virtual void Draw(IRenderer renderer)
        {
            //background
            renderer.DrawLine(
                this.Position + new Vector2(0, this.Size.Y / 2),
                this.Position + new Vector2(this.Size.X, this.Size.Y / 2),
                this.MenuStyle.BackgroundColor,
                this.Size.Y);

            var pct = Math.Min(Game.RawGameTime - this.hooverTime, 0.3f) / 0.3f;

            int alpha;
            const int MinAlpha = 170;
            if (this.isHoovered || (this is Menu menu && !menu.IsCollapsed))
            {
                alpha = (int)((pct * (255 - MinAlpha)) + MinAlpha);

                //tooltip
                if (!string.IsNullOrEmpty(this.Tooltip))
                {
                    var tooltipTextSize = this.Renderer.MeasureText(this.Tooltip, this.MenuStyle.TooltipTextSize, this.MenuStyle.Font);

                    renderer.DrawFilledRectangle(
                        new RectangleF(
                            this.Position.X + this.Size.X + this.MenuStyle.LeftIndent,
                            this.Position.Y + (tooltipTextSize.Y / 4f),
                            tooltipTextSize.X + this.MenuStyle.LeftIndent + this.MenuStyle.RightIndent,
                            tooltipTextSize.Y),
                        Color.FromArgb(200, 5, 5, 5),
                        Color.FromArgb(255, 50, 50, 50));

                    renderer.DrawText(
                        new RectangleF(
                            this.Position.X + this.Size.X + (this.MenuStyle.LeftIndent * 2),
                            this.Position.Y + (tooltipTextSize.Y / 4f),
                            tooltipTextSize.X,
                            this.Size.Y),
                        this.Tooltip,
                        Color.White,
                        RendererFontFlags.Left,
                        this.MenuStyle.TooltipTextSize);
                }
            }
            else
            {
                alpha = (int)((pct * (MinAlpha - 255)) + 255);
            }

            //name
            var textPosition = new Vector2(
                this.Position.X + this.MenuStyle.LeftIndent + this.TextIndent,
                this.Position.Y + ((this.Size.Y - this.MenuStyle.TextSize) / 3.3f));
            renderer.DrawText(
                textPosition,
                this.DisplayName,
                Color.FromArgb(Math.Max(Math.Min(alpha, 255), MinAlpha), 255, 255, 255),
                this.MenuStyle.TextSize,
                this.MenuStyle.Font);
        }
    }
}