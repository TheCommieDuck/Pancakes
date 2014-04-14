using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Input;
using WaffleCat.Core.Messages;

namespace WaffleCat.Core.Systems
{
    class InputSystem : Subsystem
    {
        public MouseInputDevice Mouse { get; private set; }

        public KeyboardInputDevice Keyboard { get; private set; }

        public GamepadInputDevice Gamepad { get; private set; }

        public InputSystem()
            :base(typeof(InputComponent))
        {
            Mouse = new MouseInputDevice();
            Keyboard = new KeyboardInputDevice();
            Gamepad = new GamepadInputDevice();
        }

        public override void Process(Entity entity, GameTime gameTime)
        {
            foreach (InputEvent e in entity.Get<InputComponent>().InputEvents.Keys)
            {
                if(e is KeyboardInputEvent)
                {
                    KeyboardInputEvent key = (KeyboardInputEvent)e;
                    if (Keyboard.CheckInput(key.Key, key.InputType))
                    {
                        //then we have a match, do something
                        Message message = entity.Get<InputComponent>().InputEvents[e]();
                        Messageboard.PostMessage(message);
                    }
                }
                else if (e is GamepadButtonInputEvent || e is GamepadAnalogStickInputEvent)
                {
                    if (Gamepad.CheckInput(e))
                    {
                        if (e is GamepadButtonInputEvent)
                        {
                            Message message = entity.Get<InputComponent>().InputEvents[e]();
                            Messageboard.PostMessage(message);
                        }
                        else
                        {
                            Vector2 stickInput = Gamepad.GetAnalogStick(((GamepadAnalogStickInputEvent)e).Stick);
                            Vector3 movement = new Vector3(stickInput.X, 0, stickInput.Y) * 3f;
                            Message message = entity.Get<InputComponent>().InputEvents[e](movement);
                            Messageboard.PostMessage(message);
                        }
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Keyboard.Update();
            Gamepad.Update();
            base.Update(gameTime);
        }
    }
}
