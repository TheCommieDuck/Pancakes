
using Microsoft.Xna.Framework.Input;
namespace WaffleCat.Core.Input
{
    public enum ButtonInput
    {
        Pressed,
        Down,
        Up,
        Released
    }

    public class InputEvent
    {
    }

    public class KeyboardInputEvent : InputEvent
    {
        public ButtonInput InputType;

        public Keys Key;
    }
}
