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
    public abstract class FeverPitchEntities : Entity//abstract class for the movement
    {

        public override Vector2 Update(Vector2 playerPos)
        {
            return playerPos;
        }

        public override void GetInput(Object source, myEventArgs args)
        {

        }

        public override void GetKeyUp(Object source, myEventArgs args)
        {

        }

        public override void GetSpacebar(Object source, myEventArgs args)
        { 
    
        }
    }
}
