#region Using Statements
using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using System.Linq;
using System.Text;
#endregion

namespace GameStateManagementSample
{
    //no collision methods cannot delcared because they are to be static
    interface ICollisionManager
    {
    }
}
