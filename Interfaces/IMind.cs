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
    public interface IMind///interface for the mind
    {
        float MySpeed { get; set; }
        int MyHealth { get; set; }

        //methods that are to be implemented
        void setHealth(int health);
        int getHealth();
        Vector2 Update(Vector2 position);
        Vector2 Update(Vector2 playerPos, Vector2 position);
    }
}
