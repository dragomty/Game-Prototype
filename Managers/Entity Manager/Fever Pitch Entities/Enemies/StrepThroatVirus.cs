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
    public class StrepThroatVirus : Enemy
    {
        StrepThroatVirusMind myMind;//mind reference

        public StrepThroatVirus()
        {
            myMind = new StrepThroatVirusMind(health);//applies the mind
            myHealth();
        }

        private void myHealth()//receives health
        {
            this.health = myMind.getHealth();
        }

        public override void Update()//receives idle update
        {
            this.position = myMind.Update(position);
        }

        public override Vector2 Update(Vector2 playerPos)//receives chase method
        {
            this.position = myMind.Update(playerPos, position);
            return playerPos;
        }
    }
}
