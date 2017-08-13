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
        private static MouseState previousMouseState;
        private static MouseState currentMouseState;
        public static void UpdateKeyboardState() {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }
        public static bool WasPressed(Keys key) {
            return previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }
        public static bool IsPressed(Keys key) {
            return currentKeyboardState.IsKeyDown(key);
        }
        public static float ScrollDifference() {
            return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;
        }
    }
}
