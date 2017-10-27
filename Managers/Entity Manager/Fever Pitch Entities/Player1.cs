#region Using Statements
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
    public class Player1 : FeverPitchEntities//player class that has access to the movement
    {
        Vector2 playerMovement;

        
        public Player1()
        {
            setHealth();
        }

        private void setHealth()//sets player health
        {
            Health = 200;
        }

        public override void GetInput(Object source, myEventArgs args)//receives the input
        {
            if (args.newKeys.Length != 0)//if key is pressed
            {
                foreach (Keys key in args.newKeys)
                {
                    //Declare the different keys that will to call the GetKeyboardInputDirection event
                    if ((key == Keys.Up) || (key == Keys.W))
                    {
                        playerMovement = Input.GetKeyboardInputDirection();
                    }
                    else if ((key == Keys.Left) || (key == Keys.A))
                    {
                        playerMovement = Input.GetKeyboardInputDirection();
                    }
                    else if ((key == Keys.Down) || (key == Keys.S))
                    {
                        playerMovement = Input.GetKeyboardInputDirection();
                    }
                    else if ((key == Keys.Right) || (key == Keys.D))
                    {
                        playerMovement = Input.GetKeyboardInputDirection();
                    }
                    else 
                    {
                        playerMovement = new Vector2(0, 0);
                    }
                }
            }
        }
        
        public override void GetKeyUp(Object source, myEventArgs args)
        {
            if (args.newKeys.Length == 0)//if none of the keys are pressed it slowly moves the player backwards
            {
                playerMovement = new Vector2(-3, 0);
            }
        }

        public override void GetSpacebar(Object source, myEventArgs args)
        {
            if (args.newKeys.Length != 0)//if keys are pressed
            {
                switch (args.newKeys[0])
                {
                    case Keys.Space://for space key
                        {
                            isBullet = true;//shoot bullet and break
                            break;
                        }
                }
            }
            /*
            if (args.newKeys.Length != 0)
            {
                if (args.newKeys[0] == Keys.Space)
                {
                    isBullet = true;
                }
                }
             * */
        }
        
        // a method that stops the player from being able to move out of bound, due to their input
        private void ScreenBounds()
        {
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 132)
                position.Y = 132;
            if (position.X > GameplayScreen.screenWidth - image.Width)
                position.X = GameplayScreen.screenWidth - image.Width;
            if (position.Y > (GameplayScreen.screenHeight - 123) - image.Height)
                position.Y = (GameplayScreen.screenHeight - 123) - image.Height;
        }

        public override void Update()
        {
            position += playerMovement; // add the Vector2 returned by GetKeyboardInputDirection to the current position of the paddle
            //make a call to the method ScreenBounds



            ScreenBounds();
        }
    }
}
