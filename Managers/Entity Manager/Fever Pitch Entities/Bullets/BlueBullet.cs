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
    public class BlueBullet : Bullet//blue bullet type class
    {

        public BlueBullet()//allows the bullet to do damage
        {
            isBulletNew = true;
            setDamage();
        }

        private void setDamage()//sets the damagae
        {
            bulletDamage = 20;
        }

        public override void Update()//moves the bullet
        {
            isBulletNew = false;
            position += new Vector2(20, 0);
        }
    }
}
