namespace O9K.Core.Managers.Input.EventArgs
{
    using System;

    using Ensage;

    using SharpDX;

    public sealed class MouseMoveEventArgs : EventArgs
    {
        public MouseMoveEventArgs(WndEventArgs args)
        {
            this.ScreenPosition = Game.MouseScreenPosition;
            this.GamePosition = Game.MousePosition;
            this.Process = args.Process;
        }

        public Vector3 GamePosition { get; }

        public bool Process { get; set; }

        public Vector2 ScreenPosition { get; }
    }
}