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
    class ColdVirusMind : EnemyMind//mind for the cold virus
    {
        public ColdVirusMind(int health)//holds viruses speed and sets his health
        {
            mySpeed = 7;
            setHealth(health);
        }

        public override void setHealth(int health)//sets entities health
        {
            health = 50;
            myHealth = health;
        }

        public override int getHealth()//receives health
        {
            return myHealth;
        }

        public override Vector2 Update(Vector2 position)//update(move)function for the cold virus
        {
            position.X -= mySpeed;
            return position;
        }
    }
}
