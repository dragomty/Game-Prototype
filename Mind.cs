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
    public class Mind : IMind
    {
        protected float mySpeed;//speed for the entities
        protected int myHealth;//health for the entities


        public float MySpeed ///allows to set the speed
        {
            get
            { return mySpeed; }
            set
            { mySpeed = value; }
        }

        public int MyHealth//allows to set the health
        {
            get
            { return myHealth; }
            set
            { myHealth = value; }
        }

        public virtual void setHealth(int health)//sets entity health
        { 
        
        }

        public virtual int getHealth()//receives entity health
        {
            return myHealth;
        }

        public virtual Vector2 Update(Vector2 position)//updates entities position
        {
            return position;
        }

        public virtual Vector2 Update(Vector2 playerPos, Vector2 position)///updates player positions
        {
            return playerPos;
        }
    }
}
