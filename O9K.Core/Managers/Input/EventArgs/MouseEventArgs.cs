namespace O9K.Core.Managers.Input.EventArgs
{
    using System;

    using Ensage;

    using Keys;

    using SharpDX;

    public sealed class MouseEventArgs : EventArgs
    {
        public MouseEventArgs(WndEventArgs args)
        {
            this.ScreenPosition = Game.MouseScreenPosition;
            this.GamePosition = Game.MousePosition;
            this.Process = args.Process;

            switch (args.Msg)
            {
                case InputManager9.WM_LBUTTONUP:
                case InputManager9.WM_LBUTTONDOWN:
                    this.MouseKey = MouseKey.Left;
                    break;

                case InputManager9.WM_RBUTTONUP:
                case InputManager9.WM_RBUTTONDOWN:
                    this.MouseKey = MouseKey.Right;
                    break;

                case InputManager9.WM_XBUTTONUP:
                case InputManager9.WM_XBUTTONDOWN:
                    this.MouseKey = args.WParam >> 16 == 1 ? MouseKey.X1 : MouseKey.X2;
                    break;

                case InputManager9.WM_MBUTTONUP:
                case InputManager9.WM_MBUTTONDOWN:
                    this.MouseKey = MouseKey.Middle;
                    break;
            }
        }

        public Vector3 GamePosition { get; }

        public MouseKey MouseKey { get; }

        public bool Process { get; set; }

        public Vector2 ScreenPosition { get; }
    }
}