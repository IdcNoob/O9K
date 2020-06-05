namespace O9K.Core.Managers.Menu.Items
{
    using System;
    using System.Windows.Input;

    using Ensage.SDK.Renderer;

    using Input;
    using Input.EventArgs;
    using Input.Keys;

    using Logger;

    using Newtonsoft.Json.Linq;

    using SharpDX;

    using Color = System.Drawing.Color;
    using KeyEventArgs = EventArgs.KeyEventArgs;

    public class MenuHoldKey : MenuItem
    {
        private bool changingKey;

        private string keyText;

        private Vector2 keyTextSize;

        private Key keyValue;

        private MouseKey mouseKeyValue;

        public MenuHoldKey(string displayName, Key key = Key.None, bool heroUnique = false)
            : this(displayName, displayName, key, heroUnique)
        {
        }

        public MenuHoldKey(string displayName, string name, Key key = Key.None, bool heroUnique = false)
            : base(displayName, name, heroUnique)
        {
            this.keyText = key.ToString();
            this.keyValue = key;
        }

        public event EventHandler<KeyEventArgs> ValueChange;

        public bool IsActive { get; private set; }

        public Key Key
        {
            get
            {
                return this.keyValue;
            }
            set
            {
                this.keyValue = value;

                if (this.SizeCalculated)
                {
                    this.keyText = this.keyValue.ToString();
                    this.keyTextSize = this.Renderer.MeasureText(this.keyText, this.MenuStyle.TextSize, this.MenuStyle.Font);
                }
            }
        }

        public MouseKey MouseKey
        {
            get
            {
                return this.mouseKeyValue;
            }
            set
            {
                this.mouseKeyValue = value;

                if (this.SizeCalculated)
                {
                    this.keyText = this.mouseKeyValue.ToString();
                    this.keyTextSize = this.Renderer.MeasureText(this.keyText, this.MenuStyle.TextSize, this.MenuStyle.Font);
                }
            }
        }

        public static implicit operator bool(MenuHoldKey item)
        {
            return item.IsActive;
        }

        public MenuHoldKey SetTooltip(string tooltip)
        {
            this.LocalizedTooltip[Lang.En] = tooltip;
            return this;
        }

        internal override void CalculateSize()
        {
            base.CalculateSize();
            this.Size = new Vector2(this.Size.X + 40, this.Size.Y);
        }

        internal override object GetSaveValue()
        {
            if (this.MouseKey != MouseKey.None)
            {
                return new
                {
                    this.MouseKey
                };
            }

            return new
            {
                this.Key
            };
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

                var key = token[nameof(this.Key)];
                if (key != null)
                {
                    this.Key = key.ToObject<Key>();
                    if (this.Key != Key.None)
                    {
                        this.InputManager.KeyDown += this.OnKeyDown;
                        this.InputManager.KeyUp += this.OnKeyUp;
                    }

                    return;
                }

                var mouseKey = token[nameof(this.MouseKey)];
                if (mouseKey != null)
                {
                    this.MouseKey = mouseKey.ToObject<MouseKey>();
                    if (this.MouseKey != MouseKey.None)
                    {
                        this.InputManager.MouseKeyDown += this.OnMouseKeyDown;
                        this.InputManager.MouseKeyUp += this.MouseKeyUp;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        internal override bool OnMouseRelease(Vector2 position)
        {
            if (this.changingKey)
            {
                return true;
            }

            if (this.IsActive)
            {
                this.IsActive = false;
                this.ValueChange?.Invoke(this, new KeyEventArgs(false, true));
            }

            this.Remove();
            this.changingKey = true;
            this.InputManager.KeyUp += this.GetKey;
            this.InputManager.MouseKeyUp += this.GetMouseKey;

            return true;
        }

        internal override void Remove()
        {
            if (this.InputManager == null)
            {
                return;
            }

            this.InputManager.KeyDown -= this.OnKeyDown;
            this.InputManager.KeyUp -= this.OnKeyUp;
            this.InputManager.MouseKeyDown -= this.OnMouseKeyDown;
            this.InputManager.MouseKeyUp -= this.MouseKeyUp;
        }

        internal override void SetInputManager(IInputManager9 inputManager)
        {
            base.SetInputManager(inputManager);

            //if (this.keyValue != Key.None)
            //{
            //    this.InputManager.KeyDown += this.OnKeyDown;
            //    this.InputManager.KeyUp += this.OnKeyUp;
            //}
            //else if (this.mouseKeyValue != MouseKey.None)
            //{
            //    this.InputManager.MouseKeyDown += this.OnMouseKeyDown;
            //    this.InputManager.MouseKeyUp += this.MouseKeyUp;
            //}

            this.InputManager.FocusChange += this.OnFocusChange;
        }

        internal override void SetRenderer(IRenderManager renderer)
        {
            base.SetRenderer(renderer);
            this.keyTextSize = this.Renderer.MeasureText(this.keyText, this.MenuStyle.TextSize, this.MenuStyle.Font);
        }

        protected override void Draw(IRenderer renderer)
        {
            var keyPosition = new Vector2(
                (this.Position.X + this.Size.X) - this.MenuStyle.RightIndent - this.keyTextSize.X,
                this.Position.Y + ((this.Size.Y - this.MenuStyle.TextSize) / 3.3f));

            //key background
            if (this.IsActive)
            {
                renderer.DrawLine(
                    this.Position + new Vector2(this.Size.X - (this.keyTextSize.X + (this.MenuStyle.RightIndent * 2)), this.Size.Y / 2),
                    this.Position + new Vector2(this.Size.X, this.Size.Y / 2),
                    this.MenuStyle.BackgroundColor,
                    this.Size.Y);
            }

            base.Draw(renderer);

            //key
            renderer.DrawText(
                keyPosition,
                this.changingKey ? "?" : this.keyText,
                Color.White,
                this.MenuStyle.TextSize,
                this.MenuStyle.Font);
        }

        private void GetKey(object sender, Input.EventArgs.KeyEventArgs e)
        {
            this.Key = e.Key == Key.Escape ? Key.None : e.Key;
            this.mouseKeyValue = MouseKey.None;
            e.Process = false;

            this.InputManager.KeyUp -= this.GetKey;
            this.InputManager.MouseKeyUp -= this.GetMouseKey;

            if (this.Key != Key.None)
            {
                this.InputManager.KeyDown += this.OnKeyDown;
                this.InputManager.KeyUp += this.OnKeyUp;
            }

            this.changingKey = false;
        }

        private void GetMouseKey(object sender, MouseEventArgs e)
        {
            if (e.MouseKey == MouseKey.Left || e.MouseKey == MouseKey.Right)
            {
                this.keyValue = Key.None;
                this.MouseKey = MouseKey.None;
            }
            else
            {
                this.keyValue = Key.None;
                this.MouseKey = e.MouseKey;
            }

            e.Process = false;

            this.InputManager.KeyUp -= this.GetKey;
            this.InputManager.MouseKeyUp -= this.GetMouseKey;

            if (this.MouseKey != MouseKey.None)
            {
                this.InputManager.MouseKeyDown += this.OnMouseKeyDown;
                this.InputManager.MouseKeyUp += this.MouseKeyUp;
            }

            this.changingKey = false;
        }

        private void MouseKeyUp(object sender, MouseEventArgs e)
        {
            if (e.MouseKey != this.mouseKeyValue)
            {
                return;
            }

            this.IsActive = false;
            this.ValueChange?.Invoke(this, new KeyEventArgs(false, true));
        }

        private void OnFocusChange(object sender, FocusChangeEventArgs e)
        {
            if (e.Active || !this.IsActive)
            {
                return;
            }

            //todo better sub + dispose ?

            this.IsActive = false;
            this.ValueChange?.Invoke(this, new KeyEventArgs(false, true));
        }

        private void OnKeyDown(object sender, Input.EventArgs.KeyEventArgs e)
        {
            if (e.Key != this.keyValue)
            {
                return;
            }

            this.IsActive = true;
            this.ValueChange?.Invoke(this, new KeyEventArgs(true, false));
        }

        private void OnKeyUp(object sender, Input.EventArgs.KeyEventArgs e)
        {
            if (e.Key != this.keyValue)
            {
                return;
            }

            this.IsActive = false;
            this.ValueChange?.Invoke(this, new KeyEventArgs(false, true));
        }

        private void OnMouseKeyDown(object sender, MouseEventArgs e)
        {
            if (e.MouseKey != this.mouseKeyValue)
            {
                return;
            }

            this.IsActive = true;
            this.ValueChange?.Invoke(this, new KeyEventArgs(true, false));
        }
    }
}