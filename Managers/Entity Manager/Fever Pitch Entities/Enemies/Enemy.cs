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
    public abstract class Enemy : Entity//abstract enemy class
    {
        protected int speed = 7;//sets the speed

        public virtual void myHealth()
        { 
        
        }

        public override Vector2 Update(Vector2 playerPos)//provides access to the player position
        {
            return playerPos;
        }
    }
}
