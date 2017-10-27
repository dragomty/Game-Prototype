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
    public class EntityManager : IEntityManager  
    {
        public EntityManager()
        {
            
        }

        //AddEntity is responsable for taking in a request of an object of type IEntity and returning it
        public IEntity AddEntity<T>() where T : IEntity, new()// the object, T, must be of type IEntity and create a new instance of that object
        {
            IEntity entity = new T();
            
            return entity;
        }

        //This method is only responsable for finding a certain object, can be used to find objects of type within a list
        public IEntity RequestEntity<T>(IEntity entity) where T : IEntity
        {
            return entity;
        }

        //responsible for taking a List of entities and removing a specific entity from it
        public void RemoveEntity<T>(List<IEntity> entities, IEntity entity) where T : IEntity
        {
            entities.Remove(entity);
        }
    }
}
