using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    static class InputHandler {
        private static KeyboardState previousKeyboardState;
        private static KeyboardState currentKeyboardState;
        public static void UpdateKeyboardState() {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
        public static bool WasPressed(Keys key) {
            return previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }
    }
}
