using UnityEngine;
using UnityEngine.InputSystem;

namespace GP.Players.ControlSources
{
    public class TestControlSource : PlayerControlSource
    {
        public override Vector2 GetMoveVector()
        {
            var keyboard = Keyboard.current;

            var input = Vector2.zero;
            if (keyboard.wKey.isPressed)
                input.y += 1f;
            if (keyboard.sKey.isPressed)
                input.y -= 1f;
            if (keyboard.aKey.isPressed)
                input.x -= 1f;
            if (keyboard.dKey.isPressed)
                input.x += 1f;

            return input;
        }
    }
}