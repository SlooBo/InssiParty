using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace InssiParty.Engine
{
    class InputManager
    {
        static KeyboardState k_state, k_state_old;
        static MouseState mouseState;

        public static void UpdateState()
        {
            k_state_old = k_state;
            k_state = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public static bool IsKeyDown(Keys key)
        {
            return k_state.IsKeyDown(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return k_state.IsKeyUp(key);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return k_state.IsKeyDown(key) && k_state_old.IsKeyUp(key);
        }

        public static bool IsKeyReleased(Keys key)
        {
            return k_state.IsKeyUp(key) && k_state_old.IsKeyDown(key);
        }

    }
}
