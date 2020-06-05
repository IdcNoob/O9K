namespace O9K.Core.Managers.Input
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics.CodeAnalysis;

    using Ensage;

    using EventArgs;

    using Keys;

    using Logger;

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [Export(typeof(IInputManager9))]
    public sealed class InputManager9 : IInputManager9
    {
        public const uint WM_ACTIVATE = 0x0006;

        public const uint WM_KEYDOWN = 0x0100;

        public const uint WM_KEYUP = 0x0101;

        public const uint WM_LBUTTONDOWN = 0x0201;

        public const uint WM_LBUTTONUP = 0x0202;

        public const uint WM_MBUTTONDOWN = 0x0207;

        public const uint WM_MBUTTONUP = 0x0208;

        public const uint WM_MOUSEMOVE = 0x0200;

        public const uint WM_MOUSEWHEEL = 0x020A;

        public const uint WM_RBUTTONDOWN = 0x0204;

        public const uint WM_RBUTTONUP = 0x0205;

        public const uint WM_SYSKEYDOWN = 0x0104;

        public const uint WM_SYSKEYUP = 0x0105;

        public const uint WM_XBUTTONDOWN = 0x020B;

        public const uint WM_XBUTTONUP = 0x020C;

        private long lastMousePosition;

        [ImportingConstructor]
        public InputManager9()
        {
            Game.OnWndProc += this.OnWndProc;
        }

        public event EventHandler<FocusChangeEventArgs> FocusChange;

        public event EventHandler<KeyEventArgs> KeyDown;

        public event EventHandler<KeyEventArgs> KeyUp;

        public event EventHandler<MouseEventArgs> MouseKeyDown;

        public event EventHandler<MouseEventArgs> MouseKeyUp;

        public event EventHandler<MouseMoveEventArgs> MouseMove;

        public event EventHandler<MouseWheelEventArgs> MouseWheel;

        private void FireFocusChange(WndEventArgs args)
        {
            if (this.FocusChange == null)
            {
                return;
            }

            var focusEventArgs = new FocusChangeEventArgs(args);

            try
            {
                this.FocusChange(this, focusEventArgs);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void FireKeyDown(WndEventArgs args)
        {
            if (this.KeyDown == null || Game.IsChatOpen /*|| Game.IsShopOpen*/)
            {
                return;
            }

            if (args.LParam >> 30 == 1)
            {
                return;
            }

            var keyEvent = new KeyEventArgs(args);

            try
            {
                this.KeyDown(this, keyEvent);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            args.Process = args.Process && keyEvent.Process;
        }

        private void FireKeyUp(WndEventArgs args)
        {
            if (this.KeyUp == null || Game.IsChatOpen /*|| Game.IsShopOpen*/)
            {
                return;
            }

            var keyEvent = new KeyEventArgs(args);

            try
            {
                this.KeyUp(this, keyEvent);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            args.Process = args.Process && keyEvent.Process;
        }

        private void FireMouseDown(WndEventArgs args)
        {
            if (this.MouseKeyDown == null)
            {
                return;
            }

            var mouseKeyEvent = new MouseEventArgs(args);
            if (mouseKeyEvent.MouseKey == MouseKey.None)
            {
                return;
            }

            try
            {
                this.MouseKeyDown(this, mouseKeyEvent);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            args.Process = args.Process && mouseKeyEvent.Process;
        }

        private void FireMouseMove(WndEventArgs args)
        {
            if (this.MouseMove == null)
            {
                return;
            }

            if (this.lastMousePosition == args.LParam)
            {
                return;
            }

            this.lastMousePosition = args.LParam;

            var moveEvent = new MouseMoveEventArgs(args);

            try
            {
                this.MouseMove(this, moveEvent);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            args.Process = args.Process && moveEvent.Process;
        }

        private void FireMouseUp(WndEventArgs args)
        {
            if (this.MouseKeyUp == null)
            {
                return;
            }

            var mouseKeyEvent = new MouseEventArgs(args);
            if (mouseKeyEvent.MouseKey == MouseKey.None)
            {
                return;
            }

            try
            {
                this.MouseKeyUp(this, mouseKeyEvent);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            args.Process = args.Process && mouseKeyEvent.Process;
        }

        private void FireMouseWheel(WndEventArgs args)
        {
            if (this.MouseWheel == null)
            {
                return;
            }

            var mouseWheelEvent = new MouseWheelEventArgs(args);

            try
            {
                this.MouseWheel(this, mouseWheelEvent);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            args.Process = args.Process && mouseWheelEvent.Process;
        }

        private void OnWndProc(WndEventArgs args)
        {
            try
            {
                switch (args.Msg)
                {
                    case WM_MOUSEMOVE:
                        this.FireMouseMove(args);
                        break;

                    case WM_SYSKEYDOWN:
                    case WM_KEYDOWN:
                        this.FireKeyDown(args);
                        break;

                    case WM_KEYUP:
                    case WM_SYSKEYUP:
                        this.FireKeyUp(args);
                        break;

                    case WM_MBUTTONUP:
                    case WM_LBUTTONUP:
                    case WM_RBUTTONUP:
                    case WM_XBUTTONUP:
                        this.FireMouseUp(args);
                        break;

                    case WM_MBUTTONDOWN:
                    case WM_LBUTTONDOWN:
                    case WM_RBUTTONDOWN:
                    case WM_XBUTTONDOWN:
                        this.FireMouseDown(args);
                        break;

                    case WM_ACTIVATE:
                        this.FireFocusChange(args);
                        break;

                    case WM_MOUSEWHEEL:
                        this.FireMouseWheel(args);
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}