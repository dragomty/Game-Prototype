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
    public interface IEntity
    {
        //variables that are to implemented
        Texture2D Image { get; set; }
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Rectangle HitBox{ get;}
        Boolean IsBullet { get; set; }
        Boolean IsBulletNew { get; set; }
        int Health { get; set; }
        int BulletDamage { get; set; }

        //methods that are to be implemented
        Vector2 centrePoint();
        float radiusFromHeight();
        float radiusFromWidth();
        void Update();
        Vector2 Update(Vector2 playerPos);
        void Draw(SpriteBatch spriteBatch);
        void GetInput(Object source, myEventArgs args); //must be present so that the event can be applied to the paddle when it is delcared as type IEntity
        void GetKeyUp(Object source, myEventArgs args);
        void GetSpacebar(Object source, myEventArgs args);
    }
}
