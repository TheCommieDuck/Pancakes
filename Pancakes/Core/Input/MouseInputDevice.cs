using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WaffleCat.Core.Input
{
    public delegate void OnMouseInput();

    public enum MouseInput
    {
        LeftButton,
        MiddleButton,
        RightButton,
        ScrollForward,
        ScrollBackward
    }

    public class MouseInputDevice : InputDevice<MouseState>
    {
        //TODO: Mouse handling.
    }
}
