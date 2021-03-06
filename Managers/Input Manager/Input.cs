﻿#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
#endregion

namespace GameStateManagementSample
{
    public class Input
    {
        public Input()
        {

        }

       
        public static Vector2 GetKeyboardInputDirection()   //PlayerIndex playerIndex)
        {
            Vector2 direction = Vector2.Zero;

            KeyboardState keyboardState = Keyboard.GetState();   //playerIndex);

            bool keyUp = false;
           

            // statements changed to OR so that the keys do not double up
                if ((keyboardState.IsKeyDown(Keys.W)) || (keyboardState.IsKeyDown(Keys.Up))) 
                {
                    direction.Y += -15; // move up
                }

                if ((keyboardState.IsKeyDown(Keys.S)) || (keyboardState.IsKeyDown(Keys.Down)))
                {
                    direction.Y += 15; // move down
                }
                
                if ((keyboardState.IsKeyDown(Keys.A)) || (keyboardState.IsKeyDown(Keys.Left)))
                {
                    direction.X += -10; // move left
                }

                if ((keyboardState.IsKeyDown(Keys.D)) || (keyboardState.IsKeyDown(Keys.Right)))
                {
                    direction.X += 15; // move right
                }

      

           
            return direction;
        }
    }
}
