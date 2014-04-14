using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WaffleCat.Core.Input
{
    class GamepadButtonInputEvent : InputEvent
    {
        public ButtonInput InputType;

        public Buttons Button;
    }

    public enum AnalogStickInput
    {
        Up,
        Down,
        Left,
        Right,
        Moved
    }

    public enum AnalogStick
    {
        Left,
        Right
    }

    class GamepadAnalogStickInputEvent : InputEvent
    {
        public AnalogStickInput InputType;

        public AnalogStick Stick;
    }
}
