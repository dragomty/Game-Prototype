using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;

namespace GameStateManagementSample
{
    class InputManager : IInputManager
    {
        //declare an event using GetKeyboardInputDirection
        public event EventHandler<myEventArgs> GetKeyboardInputDirection;

        public enum InputDevice
        {
            Keyboard
        }

        //GetInput makes use of the GetKeyboardInputDirection event
        public virtual void GetInput(Keys[] getKeys)
        {
            myEventArgs args = new myEventArgs(getKeys);
            GetKeyboardInputDirection(this, args);
        }

        //GetKeyUp makes use of the GetKeyboardInputDirection event
        public virtual void GetKeyUp(Keys[] getKeys)
        {
            myEventArgs args = new myEventArgs(getKeys);
            GetKeyboardInputDirection(this, args);
        }

        public virtual void GetSpacebar(Keys[] getKeys)
        {
            myEventArgs args = new myEventArgs(getKeys);
            GetKeyboardInputDirection(this, args);
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] getKeys = keyboardState.GetPressedKeys();

            //using the getKeys event to listen to when a key is being pressed
            if (getKeys.Length > 0)
            {
                GetInput(getKeys);
            }
            if (getKeys.Length != 0)
            {
                if (getKeys[0] == Keys.Space)
                {
                    GetSpacebar(getKeys);
                }
            }
            //using the GetKeyUp event to listen to when there are no keys being placed
            if (getKeys.Length == 0)
            {
                GetKeyUp(getKeys);
            }
        }

        public void AddListener(EventHandler<myEventArgs> handler)
        {
            //add a listener to the GetKeyboardInputDirection event
                GetKeyboardInputDirection += handler;

        }

        public void RemoveListener(EventHandler<myEventArgs> handler)
        {
            //add a listener to the GetKeyboardInputDirection event
            GetKeyboardInputDirection -= handler;
            
        }
    }
}
