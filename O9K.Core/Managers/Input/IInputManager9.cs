namespace O9K.Core.Managers.Input
{
    using System;

    using EventArgs;

    public interface IInputManager9
    {
        event EventHandler<FocusChangeEventArgs> FocusChange;

        event EventHandler<KeyEventArgs> KeyDown;

        event EventHandler<KeyEventArgs> KeyUp;

        event EventHandler<MouseEventArgs> MouseKeyDown;

        event EventHandler<MouseEventArgs> MouseKeyUp;

        event EventHandler<MouseMoveEventArgs> MouseMove;

        event EventHandler<MouseWheelEventArgs> MouseWheel;
    }
}