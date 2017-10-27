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
   public class CollisionManager : ICollisionManager
    {
       EntityManager entityMgr = new EntityManager();

       public static bool RectangleCollision(IEntity entity1, IEntity entity2)
       {
           // taking the HitBox's given in the Entity abstract class
           Rectangle rectangle1 = entity1.HitBox;
           Rectangle rectangle2 = entity2.HitBox;

           int top = Math.Max(rectangle1.Top, rectangle2.Top); // declaring the top edges of the entity's HitBox's
           int bottom = Math.Min(rectangle1.Bottom, rectangle2.Bottom);  // declaring the bottom edges of the entity's HitBox's
           int left = Math.Max(rectangle1.Left, rectangle2.Left);  // declaring the left edges of the entity's HitBox's
           int right = Math.Min(rectangle1.Right, rectangle2.Right);  // declaring the right edges of the entity's HitBox's

           if (top >= bottom || left >= right) // IF the top edge and the bottom edge of two entity's are equal / touching OR the left edge and right edge of two entity's are equal / touching
               return false; // a collision occurs

           return true;
       }

        public static bool CircleCollision(IEntity entity1, IEntity entity2) 
        {

            Vector2 V1 = entity1.centrePoint(); // position taken from the center of first entity
            Vector2 V2 = entity2.centrePoint(); // position taken from the center of second entity

            Vector2 Distance = V1 - V2; // calculating the distance between the to entities from the given points
            
            float radiusentity1 = entity1.radiusFromHeight(); // a measurement equal to the radius of the first entity (height used in this case for paddle)
            float radiusentity2 = entity2.radiusFromWidth() * 4; // a measurement equal to the radius of the second entity

            if (Distance.Length() <= radiusentity1 + radiusentity2) // IF the distance beetween the entities is smaller than both radius of the entities combined
                return true; // a collision occurs

            return false; 
        }
    }

}
