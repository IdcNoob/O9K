namespace O9K.Core.Managers.Menu
{
    using System;
    using System.Linq;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Menu.Messages;
    using Ensage.SDK.Renderer;

    using Helpers;

    using Input;

    using Items;

    using Logger;

    using Newtonsoft.Json.Linq;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal sealed class MainMenu : Menu
    {
        private readonly MenuSerializer menuSerializer;

        private bool headerDrag;

        private MenuItem hoover;

        private Vector2 menuPosition = new Vector2(300, 400);

        private Vector2 mousePressDiff;

        public MainMenu(IRenderManager renderer, IInputManager9 inputManager)
            : base("O9K", "O9K.Menu")
        {
            this.Renderer = renderer;
            this.InputManager = inputManager;
            this.MenuStyle = new MenuStyle();
            this.Language = Managers.Menu.Language.GetLanguage();

            this.IsMainMenu = true;
            this.ChildWidth = 175 * Hud.Info.ScreenRatio;

            this.CalculateSize();
            this.LoadResources();

            this.menuSerializer = new MenuSerializer();
            this.Load(this.menuSerializer.Deserialize(this));

            Messenger<RootMenuExpandMessage>.Subscribe(this.RootMenuExpandMessage);
        }

        public override bool IsVisible { get; internal set; } = true;

        internal Vector2 MenuPosition
        {
            get
            {
                return this.menuPosition;
            }
            set
            {
                var screen = Hud.Info.ScreenSize - this.Size;
                var x = Math.Max(Math.Min(value.X, screen.X), 0);
                var y = Math.Max(Math.Min(value.Y, screen.Y), 0);

                this.menuPosition = new Vector2(x, y);
            }
        }

        public override T Add<T>(T item)
        {
            base.Add(item);
            item.Load(this.menuSerializer.Deserialize(item));
            return item;
        }

        public void DrawMenu(IRenderer renderer)
        {
            this.Size = new Vector2(this.ChildWidth, this.MenuStyle.Height * 0.75f);

            renderer.DrawLine(
                this.MenuPosition + new Vector2(0, this.Size.Y / 2),
                this.MenuPosition + new Vector2(this.Size.X, this.Size.Y / 2),
                this.MenuStyle.HeaderBackgroundColor,
                this.Size.Y);

            var textPosition = new Vector2(
                (this.MenuPosition.X + this.ChildWidth) - this.DisplayNameSize.X - this.MenuStyle.RightIndent,
                this.MenuPosition.Y + ((this.Size.Y - this.MenuStyle.TextSize) / 4));

            var iconSize = new Vector2(8, 14) * Hud.Info.ScreenRatio;
            var iconPosition = new Vector2(
                textPosition.X - (6 * Hud.Info.ScreenRatio),
                this.menuPosition.Y + ((this.Size.Y - iconSize.Y) / 2));

            for (var i = 1; i <= this.MenuItems.Count; i++)
            {
                renderer.DrawTexture(this.MenuStyle.TextureIconKey, iconPosition - new Vector2(iconSize.X * i, 0), iconSize);
            }

            renderer.DrawText(textPosition, this.DisplayName, Color.White, this.MenuStyle.TextSize, this.MenuStyle.Font);

            var position = this.MenuPosition + new Vector2(0, this.Size.Y);

            for (var i = 0; i < this.MenuItems.Count; i++)
            {
                var menuItem = this.MenuItems[i];
                menuItem.OnDraw(renderer, position, this.ChildWidth);

                // line
                if (i < this.MenuItems.Count)
                {
                    renderer.DrawLine(
                        position,
                        position + new Vector2(this.ChildWidth, 0),
                        this.MenuStyle.MenuSplitLineColor,
                        this.MenuStyle.MenuSplitLineSize);
                }

                position += new Vector2(0, this.MenuStyle.Height);
            }
        }

        public void Save()
        {
            try
            {
                this.menuSerializer.Serialize(this);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        internal override MenuItem GetItemUnder(Vector2 position)
        {
            var start = this.MenuPosition;
            var end = start + this.Size;

            if (position.X >= start.X && position.X <= end.X && position.Y >= start.Y && position.Y <= end.Y)
            {
                return this;
            }

            return null;
        }

        internal override object GetSaveValue()
        {
            return new
            {
                MenuPosition = new
                {
                    this.MenuPosition.X,
                    this.MenuPosition.Y,
                }
            };
        }

        internal override void Load(JToken token)
        {
            token = token?[this.Name]?[nameof(this.MenuPosition)];
            if (token == null)
            {
                return;
            }

            this.MenuPosition = new Vector2((float)token["X"], (float)token["Y"]);
        }

        internal bool OnMouseMove(Vector2 position)
        {
            if (this.headerDrag)
            {
                this.MenuPosition = position - this.mousePressDiff;
                return false;
            }

            var noHoover = true;

            foreach (var item in this.MenuItems)
            {
                var underMouse = item.GetItemUnder(position);
                if (underMouse == null)
                {
                    continue;
                }

                noHoover = false;

                if (underMouse == this.hoover)
                {
                    continue;
                }

                this.hoover?.HooverEnd();
                this.hoover = underMouse;
                this.hoover.HooverStart();
            }

            if (noHoover && this.hoover != null)
            {
                this.hoover.HooverEnd();
                this.hoover = null;
            }

            return false;
        }

        internal override bool OnMousePress(Vector2 position)
        {
            if (this.GetItemUnder(position) != null)
            {
                this.mousePressDiff = position - this.MenuPosition;
                this.headerDrag = true;
                return true;
            }

            foreach (var item in this.MenuItems)
            {
                var underMouse = item.GetItemUnder(position);
                if (underMouse?.OnMousePress(position) == true)
                {
                    break;
                }
            }

            return false;
        }

        internal override bool OnMouseRelease(Vector2 position)
        {
            if (this.headerDrag)
            {
                this.headerDrag = false;
                return true;
            }

            foreach (var item in this.MenuItems)
            {
                var underMouse = item.GetItemUnder(position);
                if (underMouse?.OnMouseRelease(position) == true)
                {
                    if (underMouse.ParentMenu.IsMainMenu)
                    {
                        Messenger<RootMenuExpandMessage>.Publish(new RootMenuExpandMessage("O9K"));
                    }

                    break;
                }
            }

            return false;
        }

        internal override bool OnMouseWheel(Vector2 position, bool up)
        {
            foreach (var item in this.MenuItems)
            {
                var underMouse = item.GetItemUnder(position);
                if (underMouse?.OnMouseWheel(position, up) == true)
                {
                    return true;
                }
            }

            return false;
        }

        private void LoadResources()
        {
            var texture = this.Renderer.TextureManager;

            texture.LoadFromDota(this.MenuStyle.TextureArrowKey, @"panorama\images\control_icons\double_arrow_right_png.vtex_c");
            texture.LoadFromDota(this.MenuStyle.TextureLeftArrowKey, @"panorama\images\control_icons\arrow_dropdown_png.vtex_c");
            texture.LoadFromDota(this.MenuStyle.TextureIconKey, @"panorama\images\hud\reborn\tournament_pip_on_psd.vtex_c");
        }

        private void RootMenuExpandMessage(RootMenuExpandMessage args)
        {
            if (args.MainMenuName == "O9K")
            {
                return;
            }

            foreach (var menu in this.MenuItems.OfType<Menu>())
            {
                if (menu.IsCollapsed)
                {
                    continue;
                }

                menu.IsCollapsed = true;
                this.HooverEnd();

                foreach (var item in menu.Items)
                {
                    item.IsVisible = false;
                }
            }
        }
    }
}