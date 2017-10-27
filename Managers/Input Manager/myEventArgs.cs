using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;

namespace GameStateManagementSample
{
    public class myEventArgs : EventArgs 
    {
        public Keys[] newKeys;

        public myEventArgs(Keys[] getKeys)
        {
            newKeys = getKeys;
        }
    }
}
