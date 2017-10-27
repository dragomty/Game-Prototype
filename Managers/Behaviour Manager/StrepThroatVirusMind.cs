#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;
using System.Linq;
using System.Text;
#endregion

namespace GameStateManagementSample
{
    public class StrepThroatVirusMind : EnemyMind//mind class for the strepthroatvirus
    {
        private float playerDirectionY;//holds player position
        private float playerDirectionX;

        public StrepThroatVirusMind(int health)//holds the speed and sets the health
        {
            mySpeed = 6;
            setHealth(health);
        }

        public override void setHealth(int health)//sets viruses health
        {
            health = 120;
            myHealth = health;
        }

        public override int getHealth()//receives health
        {
            return myHealth;
        }

        public override Vector2 Update(Vector2 position)//idle move state for the virus which makes him move up and down
        {
            position.Y += mySpeed;

            if ((position.Y >= 450) || (position.Y <= 250))
            {
                mySpeed *= -1;
            }

            if (position.X <= 1100)
            {
                position.X += 6;
            }

            return position;
        }

        public override Vector2 Update(Vector2 playerPos, Vector2 position)//a method which provides the virus with the ability to follow and chase the player
        {
            if (mySpeed < 0)
            {
                mySpeed *= -1;
            }

            mySpeed += 0.085f;//value for the chase behaviour
            
            double x = (playerPos.X - position.X);//holds player positions
            double y = (playerPos.Y - position.Y);

            playerDirectionX = (float)(x / Math.Sqrt(x * x + y * y));//calculates an accurate player position which is used while chasing the player
            playerDirectionY = (float)(y / Math.Sqrt(x * x + y * y));

            position.X = position.X + mySpeed * playerDirectionX;//applies the chase to the strepthroat virus
            position.Y = position.Y + mySpeed * playerDirectionY;

            return position;
        }
    }
}
