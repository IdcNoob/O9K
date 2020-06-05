namespace O9K.Core.Managers.Input.EventArgs
{
    using System;

    using Ensage;

    using SharpDX;

    public sealed class MouseWheelEventArgs : EventArgs
    {
        public MouseWheelEventArgs(WndEventArgs args)
        {
            this.Position = Game.MouseScreenPosition;
            this.Up = (short)((args.WParam >> 16) & 0xFFFF) > 0;
            this.Process = args.Process;
        }

        public Vector2 Position { get; }

        public bool Process { get; set; }

        public bool Up { get; }
    }
}