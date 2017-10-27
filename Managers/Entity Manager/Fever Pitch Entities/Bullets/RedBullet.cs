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
    public class RedBullet : Bullet//red bullet class
    {
        public RedBullet()//allows the bullet to do damagae
        {
            isBulletNew = true;
            setDamage();
        }

        private void setDamage()//sets the damage
        {
            bulletDamage = 100;
        }

        public override void Update()//moves the bullet
        {
            isBulletNew = false;
            position += new Vector2(20, 0);
        }
        
    }
}

