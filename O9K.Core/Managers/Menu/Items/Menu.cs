namespace O9K.Core.Managers.Menu.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    using Input;

    using Newtonsoft.Json.Linq;

    using SharpDX;

    public class Menu : MenuItem
    {
        private bool isVisible;

        private bool lastAddedItemIsMenu = true;

        private string textureKey;

        private Vector2 textureSize;

        public Menu(string displayName)
            : this(displayName, displayName)
        {
        }

        public Menu(string displayName, string name)
            : base(displayName, name)
        {
        }

        public override bool IsVisible
        {
            get
            {
                if (this.ParentMenu.IsMainMenu)
                {
                    return true;
                }

                if (!this.ParentMenu.IsVisible)
                {
                    return false;
                }

                return this.isVisible;
            }
            internal set
            {
                this.isVisible = value;
            }
        }

        public IEnumerable<MenuItem> Items
        {
            get
            {
                return this.MenuItems;
            }
        }

        public string TextureKey
        {
            get
            {
                return this.textureKey;
            }
            set
            {
                this.textureKey = value;

                if (this.Renderer != null)
                {
                    this.LoadTexture();
                }
            }
        }

        internal bool IsCollapsed { get; set; } = true;

        internal List<MenuItem> MenuItems { get; private set; } = new List<MenuItem>();

        internal JToken Token { get; private set; }

        protected float ChildWidth { get; set; } = 150 * Hud.Info.ScreenRatio;

        public virtual T Add<T>(T item)
            where T : MenuItem
        {
            if (this.MenuItems.Contains(item))
            {
                this.Remove(item.Name);
            }

            item.ParentMenu = this;
            this.MenuItems.Add(item);
            this.OrderItems(item);

            if (this.SizeCalculated)
            {
                item.SetLanguage(this.Language);
                item.SetStyle(this.MenuStyle);
                item.SetRenderer(this.Renderer);
                item.SetInputManager(this.InputManager);
                item.CalculateSize();

                this.CalculateWidth(true);
            }

            if (this.Token != null)
            {
                item.Load(this.Token);
            }

            if (!this.IsCollapsed)
            {
                item.IsVisible = true;
            }

            return item;
        }

        public T GetOrAdd<T>(T item)
            where T : MenuItem
        {
            var addedItem = this.MenuItems.Find(x => x.Name == item.Name);
            if (addedItem != null)
            {
                return (T)addedItem;
            }

            return this.Add(item);
        }

        public void Remove(string itemName)
        {
            var item = this.MenuItems.Find(x => x.Name == itemName);
            if (item == null)
            {
                return;
            }

            this.Remove(item);
        }

        public void Remove(MenuItem item)
        {
            if (item is Menu menu)
            {
                foreach (var menuItem in menu.Items)
                {
                    menuItem.Remove();
                }
            }

            item.Remove();
            this.MenuItems.Remove(item);
        }

        public Menu SetTexture(string key)
        {
            this.TextureKey = key;
            return this;
        }

        public Menu SetTexture(AbilityId id)
        {
            return this.SetTexture(id.ToString());
        }

        public Menu SetTexture(HeroId id)
        {
            return this.SetTexture(id.ToString());
        }

        internal override void CalculateSize()
        {
            base.CalculateSize();

            foreach (var item in this.MenuItems)
            {
                item.CalculateSize();
            }
        }

        internal virtual void CalculateWidth(bool full = false)
        {
            foreach (var item in this.MenuItems)
            {
                if (!item.SizeCalculated)
                {
                    continue;
                }

                if (full && item is Menu menu)
                {
                    menu.CalculateWidth(true);
                }

                this.ChildWidth = Math.Max((int)item.Size.X, this.ChildWidth);
            }
        }

        internal override MenuItem GetItemUnder(Vector2 position)
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

            foreach (var menuItem in this.MenuItems)
            {
                var item = menuItem.GetItemUnder(position);
                if (item != null)
                {
                    return item;
                }
            }

            return null;
        }

        internal override object GetSaveValue()
        {
            return null;
        }

        internal override void Load(JToken token)
        {
            this.Token = token?[this.Name];

            foreach (var menuItem in this.MenuItems.ToList())
            {
                menuItem.Load(this.Token);
            }
        }

        internal override bool OnMouseRelease(Vector2 position)
        {
            foreach (var menu in this.ParentMenu.MenuItems.OfType<Menu>())
            {
                if (menu == this)
                {
                    continue;
                }

                if (!menu.IsCollapsed)
                {
                    menu.IsCollapsed = true;
                    menu.HooverEnd();
                }

                foreach (var item in menu.MenuItems)
                {
                    item.IsVisible = false;
                }
            }

            this.IsCollapsed = !this.IsCollapsed;

            foreach (var item in this.MenuItems)
            {
                item.IsVisible = !item.IsVisible;
            }

            return true;
        }

        internal override void Remove()
        {
            foreach (var menuItem in this.MenuItems)
            {
                menuItem.Remove();
            }
        }

        internal override void SetInputManager(IInputManager9 inputManager)
        {
            base.SetInputManager(inputManager);

            foreach (var item in this.MenuItems)
            {
                item.SetInputManager(inputManager);
            }
        }

        internal override void SetLanguage(Lang language)
        {
            base.SetLanguage(language);

            foreach (var item in this.MenuItems)
            {
                item.SetLanguage(language);
            }
        }

        internal override void SetRenderer(IRenderManager renderer)
        {
            base.SetRenderer(renderer);

            foreach (var item in this.MenuItems)
            {
                item.SetRenderer(renderer);
            }

            if (!string.IsNullOrEmpty(this.TextureKey))
            {
                this.LoadTexture();
            }
        }

        internal override void SetStyle(MenuStyle menuStyle)
        {
            base.SetStyle(menuStyle);

            foreach (var item in this.MenuItems)
            {
                item.SetStyle(menuStyle);
            }
        }

        protected override void Draw(IRenderer renderer)
        {
            base.Draw(renderer);

            //texture
            if (!string.IsNullOrEmpty(this.textureKey))
            {
                renderer.DrawTexture(
                    this.textureKey,
                    new RectangleF(
                        this.Position.X + this.MenuStyle.LeftIndent,
                        this.Position.Y + ((this.Size.Y - this.textureSize.Y) / 2),
                        this.textureSize.X,
                        this.textureSize.Y));
            }

            //arrow
            renderer.DrawTexture(
                this.MenuStyle.TextureArrowKey,
                new RectangleF(
                    (this.Position.X + this.Size.X) - this.MenuStyle.TextureArrowSize - this.MenuStyle.RightIndent,
                    this.Position.Y + ((this.Size.Y - this.MenuStyle.TextureArrowSize) / 2.2f),
                    this.MenuStyle.TextureArrowSize,
                    this.MenuStyle.TextureArrowSize));

            var position = this.Position + new Vector2(this.Size.X, 0);

            for (var i = 0; i < this.MenuItems.Count; i++)
            {
                var menuItem = this.MenuItems[i];
                menuItem.OnDraw(renderer, position, this.ChildWidth);

                if (i > 0 && i < this.MenuItems.Count && menuItem.IsVisible)
                {
                    // line
                    renderer.DrawLine(
                        position,
                        position + new Vector2(this.ChildWidth, 0),
                        this.MenuStyle.MenuSplitLineColor,
                        this.MenuStyle.MenuSplitLineSize);
                }

                position += new Vector2(0, this.MenuStyle.Height);
            }
        }

        private void LoadTexture()
        {
            if (this.textureKey.Contains("npc_dota"))
            {
                this.Renderer.TextureManager.LoadUnitFromDota(this.textureKey);
                this.textureSize = this.MenuStyle.TextureHeroSize * new Vector2(1.4f, 1.2f);
            }
            else
            {
                if (Enum.TryParse<AbilityId>(this.textureKey, out var id))
                {
                    this.Renderer.TextureManager.LoadAbilityFromDota(id);
                }

                this.textureSize = new Vector2(this.MenuStyle.TextureAbilitySize) * 1.2f;
            }

            this.TextIndent = (int)(this.textureSize.X + (this.MenuStyle.LeftIndent / 2f));
            base.CalculateSize();
            this.ParentMenu.CalculateWidth();
        }

        private void OrderItems(MenuItem item)
        {
            if (item is Menu)
            {
                if (!this.lastAddedItemIsMenu)
                {
                    this.MenuItems = this.MenuItems.OrderByDescending(x => x is Menu).ToList();
                }

                this.lastAddedItemIsMenu = this.MenuItems.LastOrDefault() is Menu;
            }
            else
            {
                this.lastAddedItemIsMenu = false;
            }
        }
    }
}