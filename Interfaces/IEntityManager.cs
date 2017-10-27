using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample
{
    interface IEntityManager
    {
        IEntity AddEntity<T>() where T : IEntity, new();
        IEntity RequestEntity<T>(IEntity entity) where T : IEntity;
        void RemoveEntity<T>(List<IEntity> entities, IEntity entity) where T : IEntity;
    }
    
}
