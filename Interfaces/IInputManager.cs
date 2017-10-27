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
    interface IInputManager
    {
        //methods that are to be implemented
        void GetInput(Keys[] getKeys);
        void GetKeyUp(Keys[] getKeys);
        void GetSpacebar(Keys[] getKeys);
        void Update();
        void AddListener(EventHandler<myEventArgs> handler);
        void RemoveListener(EventHandler<myEventArgs> handler);
    }
}
