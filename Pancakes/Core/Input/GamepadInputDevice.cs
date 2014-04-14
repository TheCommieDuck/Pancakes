using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WaffleCat.Core.Input
{
    class GamepadInputDevice : InputDevice<GamePadState>
    {
        public void Update()
        {
            base.Update(GamePad.GetState(PlayerIndex.One));
        }

        public Vector2 GetAnalogStick(AnalogStick stick)
        {
            Vector2 stickMove;
            if (stick == AnalogStick.Left)
                stickMove = this.currentState.ThumbSticks.Left;
            else
                stickMove = this.currentState.ThumbSticks.Right;
            return GetDeadZoned(stickMove);
        }
        public bool CheckInput(InputEvent ev)
        {
            if (ev is GamepadAnalogStickInputEvent)
            {
                GamepadAnalogStickInputEvent analog = ev as GamepadAnalogStickInputEvent;
                Vector2 stick = (analog.Stick == AnalogStick.Left) ? this.currentState.ThumbSticks.Left : this.currentState.ThumbSticks.Right;
                //deadzone
                stick = GetDeadZoned(stick);
                switch (analog.InputType)
                {
                    case AnalogStickInput.Moved:
                        return stick != Vector2.Zero;
                    case AnalogStickInput.Up:
                        return stick.Y < 0f;
                    case AnalogStickInput.Right:
                        return stick.X > 0f;
                    case AnalogStickInput.Left:
                        return stick.X < 0f;
                    case AnalogStickInput.Down:
                        return stick.Y > 0f;
                }
            }
            else if (ev is GamepadButtonInputEvent)
            {
                GamepadButtonInputEvent button = ev as GamepadButtonInputEvent;
                if (this.currentState.IsButtonDown(button.Button))
                {
                    switch (button.InputType)
                    {
                        case ButtonInput.Down:
                            return true;
                        case ButtonInput.Pressed:
                            return this.previousState.IsButtonUp(button.Button);
                        default:
                            return false;
                    }
                }
                else //If it's not down, it must be up.
                {
                    switch (button.InputType)
                    {
                        case ButtonInput.Up:
                            return true;
                        case ButtonInput.Released:
                            return this.previousState.IsButtonUp(button.Button);
                        default:
                            return false;
                    }
                }
            }
            else
                return false;
            return false; //???
        }

        private Vector2 GetDeadZoned(Vector2 move)
        {
            return move.Length() < 0.4f ? Vector2.Zero : move;
        }
    }
}
