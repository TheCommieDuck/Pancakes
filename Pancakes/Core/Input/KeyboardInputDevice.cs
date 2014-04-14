using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WaffleCat.Core.Input
{

    //A nicer delegate for registering.
    public delegate void OnKeyboardInput();

    public class KeyboardInputDevice : InputDevice<KeyboardState>
    {
        public void Update()
        {
            base.Update(Keyboard.GetState());
        }

        public bool CheckInput(Keys key, ButtonInput inputType)
        {
            if (this.currentState.IsKeyDown(key))
            {
                switch (inputType)
                {
                    case ButtonInput.Down:
                        return true;
                    case ButtonInput.Pressed:
                            return this.previousState.IsKeyUp(key);
                    default:
                        return false;
                }
            }
            else //If it's not down, it must be up.
            {
                switch (inputType)
                {
                    case ButtonInput.Up:
                        return true;
                    case ButtonInput.Released:
                            return this.previousState.IsKeyUp(key);
                    default:
                        return false;
                }
            }
        }
    }
}
