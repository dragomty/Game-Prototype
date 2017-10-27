using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample
{
    public class ColdVirus : Enemy//cold virus class
    {
        ColdVirusMind myMind;//reference to mind

        public ColdVirus()
        {
            myMind = new ColdVirusMind(health);//applies apropriate mind
            myHealth();
        }

        public override void myHealth()//receives health
        {
            this.health = myMind.getHealth();
        }

        public override void Update()//receives the updatae
        {
            this.position = myMind.Update(position);
        }
    }
}
