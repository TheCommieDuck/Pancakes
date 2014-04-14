using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Input;
using WaffleCat.Core.Messages;

namespace WaffleCat.Core.Entities
{
    class FPSCamera : Entity
    {
        public FPSCamera(Vector3 position, Vector3 rotation, float aspectRatio, float nearClip, float farClip)
        {
            this.AddComponent(new Transform3DComponent() { Position = position, Rotation = rotation });
            this.AddComponent(new CameraComponent(aspectRatio, nearClip, farClip));
            InputComponent input = new InputComponent();
            input.InputEvents.Add(new GamepadAnalogStickInputEvent() { InputType = AnalogStickInput.Moved, Stick = AnalogStick.Left },
                analogStick => { return new MovementMessage(this){ MovementVector = new Vector3(-analogStick.Value.X, 0f, analogStick.Value.Z) }; } );
            input.InputEvents.Add(new GamepadAnalogStickInputEvent() { InputType = AnalogStickInput.Moved, Stick = AnalogStick.Right },
                analogStick => { return new RotationMessage(this) { Rotation = new Vector3(analogStick.Value.Z, analogStick.Value.X, 0f) * -0.7f }; });
            this.AddComponent(input);
        }
    }
}
